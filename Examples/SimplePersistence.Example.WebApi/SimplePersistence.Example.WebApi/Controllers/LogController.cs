using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using SimplePersistence.Example.WebApi.Helpers;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Example.WebApi.UoW;
using SimplePersistence.UoW;

namespace SimplePersistence.Example.WebApi.Controllers
{
    public class LogController : ODataController, 
        ODataGet<Log>.WithKey<long>,
        ODataPost<Log>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IApiUnitOfWork _uow;

        public LogController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _uow = _unitOfWorkFactory.Get<IApiUnitOfWork>();
        }

        #region ODataGet

        [EnableQuery]
        public IQueryable<Log> Get()
        {
            return _uow.Logging.Logs.Query();
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] long key, CancellationToken ct)
        {
            var entity = await _uow.Logging.Logs.GetByIdAsync(key, ct);
            if (entity == null)
                return NotFound();
            return Ok(SingleResult.Create(new[] {entity}.AsQueryable()));
        }

        #endregion

        #region ODataPost

        public async Task<IHttpActionResult> Post(Log entity, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.Level = await _uow.Logging.Levels.GetByIdAsync(entity.Level.Id, ct);
            if (entity.Level == null)
                return Conflict();

            entity.Application = await _uow.Logging.Applications.GetByIdAsync(entity.Application.Id, ct);
            if (entity.Application == null)
                return Conflict();

            return Created(await _uow.ExecuteAsync(async () =>
            {
                entity.CreatedOn = DateTimeOffset.Now;
                entity.CreatedBy = User.Identity.Name;
                return await _uow.Logging.Logs.AddAsync(entity, ct);
            }, ct));
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