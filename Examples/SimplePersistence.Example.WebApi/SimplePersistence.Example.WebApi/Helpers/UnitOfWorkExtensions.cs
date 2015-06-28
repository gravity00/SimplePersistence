using System;
using System.Threading;
using System.Threading.Tasks;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.Helpers
{
    public static class UnitOfWorkExtensions
    {
        public static async Task<T> ExecuteAsync<T>(this IUnitOfWork uow, Func<Task<T>> toExecute, CancellationToken ct)
        {
            await uow.BeginAsync(ct);

            var result = await toExecute();

            await uow.CommitAsync(ct);

            return result;
        } 
    }
}