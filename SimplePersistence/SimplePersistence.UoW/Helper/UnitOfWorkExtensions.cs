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
namespace SimplePersistence.UoW.Helper
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;

    /// <summary>
    /// Extension classes for the Unit of Work pattern
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        #region ExecuteAndCommitAsync

        #region Task<T>

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<T>(this IUnitOfWork uow, Func<Task<T>> toExecute)
        {
            return uow.ExecuteAndCommitAsync(toExecute, CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<T>(this IUnitOfWork uow, Func<Task<T>> toExecute, CancellationToken ct)
        {
            return uow.ExecuteAndCommitAsync((w, c) => toExecute(), ct);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<T>(this IUnitOfWork uow, Func<IUnitOfWork, CancellationToken, Task<T>> toExecute)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            return uow.ExecuteAndCommitAsync(toExecute, CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<T>(this IUnitOfWork uow, Func<IUnitOfWork, CancellationToken, Task<T>> toExecute, CancellationToken ct)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            return ExecuteAndCommitAsync<IUnitOfWork, T>(uow, toExecute, ct);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<TUoW, T>(this TUoW uow, Func<TUoW, CancellationToken, Task<T>> toExecute)
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync(toExecute, CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task<T> ExecuteAndCommitAsync<TUoW,T>(this TUoW uow, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var tcs = new TaskCompletionSource<T>();
            uow.BeginAsync(ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    toExecute(uow, ct).ContinueWith(t02 =>
                    {
                        if (t02.IsFaulted)
                        {
                            tcs.SetExceptionFromTask(t02);
                        }
                        else if (t02.IsCompleted)
                        {
                            uow.CommitAsync(ct).ContinueWith(t03 =>
                            {
                                if (t03.IsFaulted)
                                {
                                    tcs.SetExceptionFromTask(t03);
                                }
                                else if (t03.IsCompleted)
                                {
                                    tcs.SetResult(t02.Result);
                                }
                                else
                                {
                                    tcs.SetCanceled();
                                }
                            }, ct);
                        }
                        else
                        {
                            tcs.SetCanceled();
                        }
                    }, ct);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }

        #endregion

        #region Task

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync(this IUnitOfWork uow, Func<Task> toExecute)
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync(this IUnitOfWork uow, Func<Task> toExecute, CancellationToken ct)
        {
            return uow.ExecuteAndCommitAsync((u, c) => toExecute(), ct);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync(this IUnitOfWork uow, Func<IUnitOfWork, CancellationToken, Task> toExecute)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            return uow.ExecuteAndCommitAsync(toExecute, CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync(this IUnitOfWork uow, Func<IUnitOfWork, CancellationToken, Task> toExecute, CancellationToken ct)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            return ExecuteAndCommitAsync<IUnitOfWork>(uow, toExecute, ct);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync<TUoW>(this TUoW uow, Func<TUoW, CancellationToken, Task> toExecute)
            where TUoW : IUnitOfWork
        {
            return uow.ExecuteAndCommitAsync(toExecute, CancellationToken.None);
        }

        /// <summary>
        /// Executes the given asynchronous function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static Task ExecuteAndCommitAsync<TUoW>(this TUoW uow, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            var tcs = new TaskCompletionSource<bool>();
            uow.BeginAsync(ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetExceptionFromTask(t01);
                }
                else if (t01.IsCompleted)
                {
                    toExecute(uow, ct).ContinueWith(t02 =>
                    {
                        if (t02.IsFaulted)
                        {
                            tcs.SetExceptionFromTask(t02);
                        }
                        else if (t02.IsCompleted)
                        {
                            uow.CommitAsync(ct).ContinueWith(t03 =>
                            {
                                if (t03.IsFaulted)
                                {
                                    tcs.SetExceptionFromTask(t03);
                                }
                                else if (t03.IsCompleted)
                                {
                                    tcs.SetResult(true);
                                }
                                else
                                {
                                    tcs.SetCanceled();
                                }
                            }, ct);
                        }
                        else
                        {
                            tcs.SetCanceled();
                        }
                    }, ct);
                }
                else
                {
                    tcs.SetCanceled();
                }
            }, ct);

            return tcs.Task;
        }

        #endregion

        #endregion

        #region ExecuteAndCommit

        #region T

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T ExecuteAndCommit<T>(this IUnitOfWork uow, Func<T> toExecute)
        {
            return uow.ExecuteAndCommit(u => toExecute());
        }

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T ExecuteAndCommit<T>(this IUnitOfWork uow, Func<IUnitOfWork, T> toExecute)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            return ExecuteAndCommit<IUnitOfWork, T>(uow, toExecute);
        }

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T ExecuteAndCommit<TUoW,T>(this TUoW uow, Func<TUoW, T> toExecute)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            uow.Begin();

            var result = toExecute(uow);

            uow.Commit();
            return result;
        }

        #endregion

        #region Void

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void ExecuteAndCommit(this IUnitOfWork uow, Action toExecute)
        {
            uow.ExecuteAndCommit(u => toExecute());
        }

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void ExecuteAndCommit(this IUnitOfWork uow, Action<IUnitOfWork> toExecute)
        {
            //  bug fix https://github.com/gravity00/SimplePersistence/issues/5
            ExecuteAndCommit<IUnitOfWork>(uow, toExecute);
        }

        /// <summary>
        /// Executes the given lambda inside an <see cref="IUnitOfWork.Begin"/> 
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The code to be executed inside the scope</param>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void ExecuteAndCommit<TUoW>(this TUoW uow, Action<TUoW> toExecute)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (toExecute == null) throw new ArgumentNullException("toExecute");

            uow.Begin();

            toExecute(uow);

            uow.Commit();
        }

        #endregion

        #endregion
    }
}
