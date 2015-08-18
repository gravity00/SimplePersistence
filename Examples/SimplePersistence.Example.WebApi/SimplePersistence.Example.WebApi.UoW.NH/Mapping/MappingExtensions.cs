using System;
using FluentNHibernate.Mapping;
using SimplePersistence.Model;

namespace SimplePersistence.Example.WebApi.UoW.NH.Mapping
{
    [CLSCompliant(false)]
    public static class MappingExtensions
    {
        /// <summary>
        /// The default (128) max length used in metadata fields
        /// </summary>
        public const int DefaultMaxLength = 128;


        public static ClassMap<T> MapCreatedMeta<T>(
            this ClassMap<T> classMap, int maxLength = DefaultMaxLength, string propertyCreatedOnIndex = null, 
            Action<PropertyPart, PropertyPart> extraConfiguration = null)
            where T : IHaveCreatedMeta<string>
        {
            var createdOnMap = classMap.Map(e => e.CreatedOn).Not.Nullable();
            if (propertyCreatedOnIndex != null)
                createdOnMap.Index(propertyCreatedOnIndex);

            var createdByMap = classMap.Map(e => e.CreatedBy).Nullable().Length(maxLength);

            if (extraConfiguration != null)
                extraConfiguration(createdOnMap, createdByMap);

            return classMap;
        }

        public static ClassMap<T> MapUpdatedMeta<T>(
            this ClassMap<T> classMap, int maxLength = DefaultMaxLength, string propertyUpdatedOnIndex = null,
            Action<PropertyPart, PropertyPart> extraConfiguration = null)
            where T : IHaveUpdatedMeta<string>
        {
            var updatedOnMap = classMap.Map(e => e.UpdatedOn).Not.Nullable();
            if (propertyUpdatedOnIndex != null)
                updatedOnMap.Index(propertyUpdatedOnIndex);

            var updatedByMap = classMap.Map(e => e.UpdatedBy).Nullable().Length(maxLength);

            if (extraConfiguration != null)
                extraConfiguration(updatedOnMap, updatedByMap);

            return classMap;
        }

        public static ClassMap<T> MapDeletedMeta<T>(
            this ClassMap<T> classMap, int maxLength = DefaultMaxLength, string propertyDeletedOnIndex = null,
            Action<PropertyPart, PropertyPart> extraConfiguration = null)
            where T : IHaveDeletedMeta<string>
        {
            var deletedOnMap = classMap.Map(e => e.DeletedOn).Nullable();
            if (propertyDeletedOnIndex != null)
                deletedOnMap.Index(propertyDeletedOnIndex);

            var deletedByMap = classMap.Map(e => e.DeletedBy).Nullable().Length(maxLength);

            if (extraConfiguration != null)
                extraConfiguration(deletedOnMap, deletedByMap);

            return classMap;
        }

        public static ClassMap<T> MapLongVersion<T>(this ClassMap<T> classMap, Action<VersionPart> extraConfiguration = null)
            where T : IHaveVersionAsLong
        {
            var longVersionMap = classMap.Version(x => x.Version).Generated.Never().UnsavedValue("0").Not.Nullable();
            classMap.OptimisticLock.Version();

            if (extraConfiguration != null)
                extraConfiguration(longVersionMap);

            return classMap;
        }

        public static ClassMap<T> MapByteArrayVersion<T>(this ClassMap<T> classMap, Action<VersionPart> extraConfiguration = null)
            where T : IHaveVersionAsByteArray
        {
            var longVersionMap =
                classMap.Version(x => x.Version).Generated.Always().UnsavedValue("null").Not.Nullable();
            classMap.OptimisticLock.Version();

            if (extraConfiguration != null)
                extraConfiguration(longVersionMap);

            return classMap;
        }
    }
}
