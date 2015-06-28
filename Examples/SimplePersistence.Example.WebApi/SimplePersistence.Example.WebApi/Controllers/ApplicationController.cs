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
    public class ApplicationController : ODataController, ODataGet<Application>.WithKey<string>,
        ODataPost<Application>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IApiUnitOfWork _uow;

        public ApplicationController(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _uow = _unitOfWorkFactory.Get<IApiUnitOfWork>();
        }

        #region ODataGet

        [EnableQuery]
        public IQueryable<Application> Get()
        {
            return _uow.Logging.Applications.Query();
        }

        [EnableQuery]
        public async Task<IHttpActionResult> Get([FromODataUri] string key, CancellationToken ct)
        {
            var entity = await _uow.Logging.Applications.GetByIdAsync(key, ct);
            if (entity == null)
                return NotFound();
            return Ok(SingleResult.Create(new[] {entity}.AsQueryable()));
        }

        #endregion

        #region ODataPost

        public async Task<IHttpActionResult> Post(Application entity, CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Created(await _uow.ExecuteAsync(async () =>
            {
                entity.CreatedOn = entity.UpdatedOn = DateTimeOffset.Now;
                entity.CreatedBy = entity.UpdatedBy = User.Identity.Name;
                return await _uow.Logging.Applications.AddAsync(entity, ct);
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