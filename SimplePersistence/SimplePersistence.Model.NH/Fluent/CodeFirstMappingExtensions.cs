#region License
// The MIT License (MIT)
// Copyright (c) 2015 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using FluentNHibernate.Mapping;

namespace SimplePersistence.Model.NH.Fluent
{
    /// <summary>
    /// Extension methods for NHibernate code first mappings
    /// </summary>
    public static class CodeFirstMappingExtensions
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

            extraConfiguration?.Invoke(createdOnMap, createdByMap);

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

            extraConfiguration?.Invoke(updatedOnMap, updatedByMap);

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

            extraConfiguration?.Invoke(deletedOnMap, deletedByMap);

            return classMap;
        }

        public static ClassMap<T> MapLongVersion<T>(this ClassMap<T> classMap, Action<VersionPart> extraConfiguration = null)
            where T : IHaveVersionAsLong
        {
            var longVersionMap = classMap.Version(x => x.Version).Generated.Never().UnsavedValue("0").Not.Nullable();
            classMap.OptimisticLock.Version();

            extraConfiguration?.Invoke(longVersionMap);

            return classMap;
        }

        public static ClassMap<T> MapByteArrayVersion<T>(this ClassMap<T> classMap, Action<VersionPart> extraConfiguration = null)
            where T : IHaveVersionAsByteArray
        {
            var longVersionMap =
                classMap.Version(x => x.Version).Generated.Always().UnsavedValue("null").Not.Nullable();
            classMap.OptimisticLock.Version();

            extraConfiguration?.Invoke(longVersionMap);

            return classMap;
        }
    }
}
