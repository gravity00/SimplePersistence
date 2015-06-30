using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimplePersistence.UoW.Helper
{
    /// <summary>
    /// Extension classes for the Unit of Work pattern
    /// </summary>
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="toExecute"></param>
        /// <param name="ct"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteAndCommitAsync<T>(this IUnitOfWork uow, Func<Task<T>> toExecute, CancellationToken ct)
        {
            var tcs = new TaskCompletionSource<T>();
            uow.BeginAsync(ct).ContinueWith(t01 =>
            {
                if (t01.IsFaulted)
                {
                    tcs.SetException(t01.Exception);
                }
                else if (t01.IsCompleted)
                {
                    toExecute().ContinueWith(t02 =>
                    {
                        if (t02.IsFaulted)
                        {
                            tcs.SetException(t02.Exception);
                        }
                        else if (t02.IsCompleted)
                        {
                            uow.CommitAsync(ct).ContinueWith(t03 =>
                            {
                                if (t03.IsFaulted)
                                {
                                    tcs.SetException(t03.Exception);
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
    }
}
