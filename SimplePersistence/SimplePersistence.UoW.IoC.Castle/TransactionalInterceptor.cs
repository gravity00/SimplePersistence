#region License
// The MIT License (MIT)
// Copyright (c) 2015 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion
using Castle.DynamicProxy;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;

namespace SimplePersistence.UoW.IoC.Castle
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Transactions;
    using Aspects;

    public class TransactionalInterceptor : IInterceptor
    {
        #region Mappers

        private static readonly Lazy<MethodInfo> TaskMapperFactoryMethod = new Lazy<MethodInfo>(
            () =>
                typeof(InterceptorExtensions).GetMethods()
                    .Single(mi => mi.Name == "InjectTransactionalInterceptCodeAndReturnNewTask" && mi.IsGenericMethod));

        private static readonly ConcurrentDictionary<Type, Func<Task, IUnitOfWork, Action, Action, CancellationToken, Task>> MappersCache =
            new ConcurrentDictionary<Type, Func<Task, IUnitOfWork, Action, Action, CancellationToken, Task>>();

        public static readonly Func<Type, Task, IUnitOfWork, Action, Action, CancellationToken, Task> DefaultTaskMapper
            =
            (type, task, uow, preReturnCode, finalizationCode, ct) =>
            {
                //  implementações de Task<int>, Task<string>, etc, já existem na classe base
                //  por isso se faltar alguma é favor adicionar lá, tipo Task<decimal>
                task =
                    MappersCache.GetOrAdd(
                        type,
                        t =>
                        {
                            var taskParam = Expression.Parameter(typeof(Task), "task");
                            var uowParam = Expression.Parameter(typeof(IUnitOfWork), "uow");
                            var preReturnCodeParam = Expression.Parameter(typeof(Action), "preReturnCode");
                            var finalizationCodeParam = Expression.Parameter(typeof(Action), "finalizationCode");
                            var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

                            var interceptAndReturnAsTaskMethodCall =
                                TaskMapperFactoryMethod.Value.MakeGenericMethod(t.GetGenericArguments()[0]);

                            var interceptAndReturnAsTaskCall =
                                Expression.Call(
                                    interceptAndReturnAsTaskMethodCall,
                                    taskParam,
                                    uowParam,
                                    preReturnCodeParam,
                                    finalizationCodeParam,
                                    ctParam);

                            var returnFunc =
                                Expression.Lambda<Func<Task, IUnitOfWork, Action, Action, CancellationToken, Task>>(
                                    interceptAndReturnAsTaskCall,
                                    taskParam,
                                    uowParam,
                                    preReturnCodeParam,
                                    finalizationCodeParam,
                                    ctParam).Compile();

                            return returnFunc;
                        })(task, uow, preReturnCode, finalizationCode, ct);

                return task;
            };

        #endregion

        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IWindsorContainer _windsorContainer;
        private readonly Func<Type, Task, IUnitOfWork, Action, Action, CancellationToken, Task> _taskMapper;

        public TransactionalInterceptor(
            IUnitOfWorkFactory unitOfWorkFactory, IWindsorContainer windsorContainer,
            Func<Type, Task, IUnitOfWork, Action, Action, CancellationToken, Task> taskMapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _windsorContainer = windsorContainer;
            _taskMapper = taskMapper;
        }

        public void Intercept(IInvocation invocation)
        {
            var ta =
                (TransactionalAttribute)
                    invocation.MethodInvocationTarget.GetCustomAttributes(typeof(TransactionalAttribute), true)
                        .SingleOrDefault();

            //  If no transactional attributes or not required, proceed as usual
            if (ta == null || ta.TransactionType == TransactionType.NotRequired)
            {
                invocation.Proceed();
                return;
            }

            //  if it is an async invocation
            if (typeof(Task).IsAssignableFrom(invocation.Method.ReturnType))
            {
                //  try to get a cancellation token, starting from the last parameter
                //  (normally the cancellation token is passed as the last parameter)
                var ct = CancellationToken.None;
                for (var index = invocation.Arguments.Length-1; index >= 0; index--)
                {
                    var argument = invocation.Arguments[index];
                    // ReSharper disable once InvertIf
                    if (argument is CancellationToken)
                    {
                        ct = (CancellationToken) argument;
                        break;
                    }
                }

                //  intercept a task method
                InterceptTaskMethod(invocation, ta, ct);
            }
            else
            {
                //  else intercept as a sync method
                InterceptCommonMethod(invocation, ta);
            }
        }

        private void InterceptTaskMethod(IInvocation invocation, TransactionalAttribute ta, CancellationToken ct)
        {
            IDisposable ws;
            TransactionScopeOption tso;
            switch (ta.TransactionType)
            {
                case TransactionType.RequiresNew:
                    ws = _windsorContainer.BeginScope();
                    tso = TransactionScopeOption.RequiresNew;
                    break;
                default:
                    ws = _windsorContainer.RequireScope();
                    tso = TransactionScopeOption.Required;
                    break;
            }

            Action finalizationCode;
            Action preReturnCode = null;
            if (ta.UseGlobalTransaction)
            {
                TransactionScope ts;
                try
                {
                    ts = new TransactionScope(tso, TimeSpan.FromMinutes(ta.TransactionDurationLimit));
                }
                catch
                {
                    if (ws != null)
                        ws.Dispose();
                    throw;
                }
                preReturnCode = () => ts.Complete();
                finalizationCode =
                    () =>
                    {
                        if (ts != null)
                            ts.Dispose();
                        if (ws != null)
                            ws.Dispose();
                    };
            }
            else
            {
                finalizationCode =
                    () =>
                    {
                        if (ws != null)
                            ws.Dispose();
                    };
            }

            try
            {
                var uow = _unitOfWorkFactory.Get(ta.UnitOfWorkType);
                uow.Begin();

                invocation.Proceed();
                var task = (Task)invocation.ReturnValue;

                //  if Task<TResult>
                var returnValueType = invocation.Method.ReturnType;
                if (returnValueType.IsGenericType)
                {
                    #region Base Type Handlers

                    if (returnValueType == typeof(Task<int>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<int>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<long>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<long>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<double>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<double>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<float>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<float>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<string>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<string>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<decimal>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<decimal>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    else if (returnValueType == typeof(Task<bool>))
                    {
                        task = task.InjectTransactionalInterceptCodeAndReturnNewTask<bool>(uow, preReturnCode,
                            finalizationCode, ct);
                    }
                    #endregion
                    else
                    {
                        try
                        {
                            task =
                                _taskMapper(returnValueType, task, uow, preReturnCode, finalizationCode, ct);
                        }
                        catch (Exception e)
                        {
                            throw new TaskMappingException(returnValueType, e);
                        }
                        if (task == null)
                            throw new TaskMappingException(returnValueType);
                    }
                }
                else
                {
                    //  if Task
                    task =
                        task.InjectTransactionalInterceptCodeAndReturnNewTask(
                            uow, preReturnCode, finalizationCode, ct);
                }
                invocation.ReturnValue = task;
            }
            catch
            {
                finalizationCode();
                throw;
            }
        }

        private void InterceptCommonMethod(IInvocation invocation, TransactionalAttribute ta)
        {
            IDisposable ws;
            TransactionScopeOption tso;
            switch (ta.TransactionType)
            {
                case TransactionType.RequiresNew:
                    ws = _windsorContainer.BeginScope();
                    tso = TransactionScopeOption.RequiresNew;
                    break;
                default:
                    ws = _windsorContainer.RequireScope();
                    tso = TransactionScopeOption.Required;
                    break;
            }

            using (ws)
            {
                var uow = _unitOfWorkFactory.Get(ta.UnitOfWorkType);
                if (ta.UseGlobalTransaction)
                {
                    using (var ts = new TransactionScope(tso, TimeSpan.FromMinutes(ta.TransactionDurationLimit)))
                    {
                        uow.Begin();
                        invocation.Proceed();
                        uow.Commit();
                        ts.Complete();
                    }
                }
                else
                {
                    uow.Begin();
                    invocation.Proceed();
                    uow.Commit();
                }
            }
        }
    }
}
