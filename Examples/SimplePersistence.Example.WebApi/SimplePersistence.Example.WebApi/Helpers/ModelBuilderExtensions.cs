using System;
using System.Linq;
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

        public static EntityTypeConfiguration<T> EntityType<T>(this EntitySetConfiguration<T> cfg, Action<EntityTypeConfiguration<T>> action)
            where T : class
        {
            var entityType = cfg.EntityType;
            action(entityType);
            return entityType;
        }

        public static void ExcludeDuplicatedVersionPropertiesNotSetAsConcurrencyToken(this StructuralTypeConfiguration structuralType)
        {
            var properties = structuralType.Properties.ToArray();
            foreach (var ppc in
                properties
                    .Where(e => e.Name == "Version")
                    .OfType<PrimitivePropertyConfiguration>()
                    .Where(e => !e.ConcurrencyToken))
            {
                structuralType.RemoveProperty(ppc.PropertyInfo);
            }
        }
    }
}