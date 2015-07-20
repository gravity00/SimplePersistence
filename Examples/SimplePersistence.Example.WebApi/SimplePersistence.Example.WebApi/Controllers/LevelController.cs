using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW;
using SimplePersistence.UoW;
using SimplePersistence.UoW.Exceptions;
using SimplePersistence.UoW.Helper;

namespace SimplePersistence.Example.WebApi.Controllers
{
    public class LevelController : ODataController, ODataGet<Level>.WithKey<string>, ODataPut<Level>.WithKey<string>, ODataPatch<Level>.WithKey<string>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IApiUnitOfWork _uow;

        public LevelController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _uow = _unitOfWorkFactory.Get<IApiUnitOfWork>();
        }

        #region ODataGet

        [EnableQuery]
        public IQueryable<Level> Get()
        {
            return _uow.Logging.Levels.Query();
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] string key, CancellationToken ct)
        {
            var entity = await _uow.Logging.Levels.GetByIdAsync(key, ct);
            if (entity == null)
                return NotFound();
            return Ok(SingleResult.Create(new[] {entity}.AsQueryable()));
        }

        #endregion

        #region ODataPut

        public async Task<IHttpActionResult> Put([FromODataUri] string key, Level update, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (key != update.Id)
                return BadRequest();

            Level result;
            try
            {
                result = await _uow.ExecuteAndCommitAsync(
                    async () => await _uow.Logging.Levels.UpdateAsync(update, ct), ct);
            }
            catch (ConcurrencyException)
            {
                if (_uow.Logging.Levels.Exists(key))
                    throw;
                return NotFound();
            }
            return Updated(result);
        }

        #endregion

        #region ODataPatch

        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Level> entity, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbEntity = await _uow.Logging.Levels.GetByIdAsync(key, ct);
            if (dbEntity == null)
                return NotFound();

            Level result;
            try
            {
                entity.Patch(dbEntity);
                result = await _uow.ExecuteAndCommitAsync(
                    async () => await _uow.Logging.Levels.UpdateAsync(dbEntity, ct), ct);
            }
            catch (ConcurrencyException)
            {
                if (_uow.Logging.Levels.Exists(key))
                    throw;
                return NotFound();
            }
            return Updated(result);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
                _unitOfWorkFactory.Release(_uow);
        }
    }
}