using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace SimplePersistence.Example.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            #region OData Registration

            var builder = new ODataConventionModelBuilder();

            config.MapODataServiceRoute("ODataRoute", "odata", builder.GetEdmModel());

            #endregion
        }
    }
}
