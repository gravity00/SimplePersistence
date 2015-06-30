using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace SimplePersistence.Example.WebApi.Controllers
{
    /// <summary>
    /// HTTP Get OData interfaces
    /// </summary>
    public static class ODataGet<TEntity>
    {
        /// <summary>
        /// Using T as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey>
        {
            /// <summary>
            /// Gets an <see cref="IQueryable{T}"/> of the given entity
            /// </summary>
            /// <returns></returns>
            [EnableQuery]
            IQueryable<TEntity> Get();

            /// <summary>
            /// Gets an <see cref="IQueryable{T}"/> of the given entity
            /// </summary>
            /// <returns></returns>
            [EnableQuery]
            Task<IHttpActionResult> Get([FromODataUri] TKey key, CancellationToken ct);
        }

        /// <summary>
        /// Using T as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey1, in TKey2>
        {
            /// <summary>
            /// Gets an <see cref="IQueryable{T}"/> of the given entity
            /// </summary>
            /// <returns></returns>
            [EnableQuery]
            IQueryable<TEntity> Get();

            /// <summary>
            /// Gets an <see cref="IQueryable{T}"/> of the given entity
            /// </summary>
            /// <returns></returns>
            [EnableQuery]
            Task<IHttpActionResult> Get([FromODataUri] TKey1 key1, [FromODataUri] TKey2 key2, CancellationToken ct);
        }
    }

    /// <summary>
    /// HTTP Post OData interfaces
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface ODataPost<in TEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IHttpActionResult> Post(TEntity entity, CancellationToken ct);
    }

    /// <summary>
    /// HTTP Patch OData interfaces
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ODataPatch<TEntity> where TEntity : class
    {
        /// <summary>
        /// Using TKey as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="entity"></param>
            /// <param name="ct"></param>
            /// <returns></returns>
            Task<IHttpActionResult> Patch([FromODataUri] TKey key, Delta<TEntity> entity, CancellationToken ct);
        }
    }

    /// <summary>
    /// HTTP Put OData interfaces
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ODataPut<TEntity> where TEntity : class
    {
        /// <summary>
        /// Using a TKey as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="update"></param>
            /// <param name="ct"></param>
            /// <returns></returns>
            Task<IHttpActionResult> Put([FromODataUri] TKey key, TEntity update, CancellationToken ct);
        }

    }

    /// <summary>
    /// HTTP Delete OData interfaces
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ODataDelete
    {
        /// <summary>
        /// Using a long as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey>
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="key"></param>
            /// <param name="ct"></param>
            /// <returns></returns>
            Task<IHttpActionResult> Delete([FromODataUri] TKey key, CancellationToken ct);
        }

        /// <summary>
        /// Using a long as a key
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public interface WithKey<in TKey1, in TKey2>
        {
            Task<IHttpActionResult> Delete([FromODataUri] TKey1 key1, [FromODataUri] TKey2 key2, CancellationToken ct);
        }
    }
}
