using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using SimplePersistence.Example.WebApi.Helpers;
using SimplePersistence.Example.WebApi.Models.Logging;

namespace SimplePersistence.Example.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            #region OData Registration

            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Application>();

            config.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

            #endregion
        }
    }
}
