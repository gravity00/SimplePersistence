using System;
using System.Web.OData.Builder;

namespace SimplePersistence.Example.WebApi.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static EntitySetConfiguration<T> EntitySet<T>(this ODataModelBuilder builder, Action<EntitySetConfiguration<T>> action)
            where T : class
        {
            var entitySet = builder.EntitySet<T>();
            action(entitySet);
            return entitySet;
        }

        public static EntitySetConfiguration<T> EntitySet<T>(this ODataModelBuilder builder)
            where T : class
        {
            return builder.EntitySet<T>(typeof(T).Name);
        }
    }
}