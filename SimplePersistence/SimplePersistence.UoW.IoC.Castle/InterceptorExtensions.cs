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
namespace SimplePersistence.UoW.IoC.Castle
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public static class InterceptorExtensions
    {
        public static Task InjectTransactionalInterceptCodeAndReturnNewTask(
            this Task task, IUnitOfWork uow, Action preReturnCode, Action finalizationCode, CancellationToken ct)
        {
            return task
                .ContinueWith(
                    taskIntercepted =>
                    {
                        try
                        {
                            taskIntercepted.ThrowIfFaulted<Exception>();
                            return uow.CommitAsync(ct)
                                .ContinueWith(
                                    taskCommit =>
                                    {
                                        try
                                        {
                                            taskCommit.ThrowIfFaulted<Exception>();
                                            if (preReturnCode != null)
                                                preReturnCode();
                                        }
                                        finally
                                        {
                                            if (finalizationCode != null)
                                                finalizationCode();
                                        }
                                    }, ct);
                        }
                        catch
                        {
                            if (finalizationCode != null)
                                finalizationCode();
                            throw;
                        }
                    }, ct).Unwrap();
        }

        public static Task<TResult> InjectTransactionalInterceptCodeAndReturnNewTask<TResult>(
            this Task task, IUnitOfWork uow, Action preReturnCode, Action finalizationCode, CancellationToken ct)
        {
            var taskCasted = (Task<TResult>)task;
            return taskCasted
                .ContinueWith(
                    taskIntercepted =>
                    {
                        try
                        {
                            taskIntercepted.ThrowIfFaulted<Exception>();
                            var result = taskIntercepted.Result;

                            return uow.CommitAsync(ct)
                                .ContinueWith(
                                    taskCommit =>
                                    {
                                        try
                                        {
                                            taskCommit.ThrowIfFaulted<Exception>();
                                            if (preReturnCode != null)
                                                preReturnCode();
                                            return result;
                                        }
                                        finally
                                        {
                                            if (finalizationCode != null)
                                                finalizationCode();
                                        }
                                    }, ct);
                        }
                        catch
                        {
                            if (finalizationCode != null)
                                finalizationCode();
                            throw;
                        }
                    }, ct).Unwrap();
        }

        public static void ThrowIfFaulted<TException>(this Task task)
            where TException : Exception, new()
        {
            if (!task.IsFaulted) return;

            if (task.Exception == null)
                throw new TException();
            throw task.Exception;
        }
    }
}
