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
namespace SimplePersistence.UoW.EF.Helper
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using Model;

    /// <summary>
    /// Extension methods for Entity Framework code first mappings
    /// </summary>
    public static class CodeFirstMappingExtensions
    {
        /// <summary>
        /// The default (128) max length used in metadata fields
        /// </summary>
        public const int DefaultMaxLength = 128;

        /// <summary>
        /// The default value when a property can have an index. By default is false.
        /// </summary>
        public const bool DefaultPropertyNeedsIndex = false;

        #region Entity

        /// <summary>
        /// Sets up a new entity in the given model and applies the configuration lambda to it
        /// </summary>
        /// <param name="modelBuilder">The model builder to be used</param>
        /// <param name="configurations">The lambda which applies the configuration</param>
        /// <typeparam name="TEntityType"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeConfiguration<TEntityType> Entity<TEntityType>(
            this DbModelBuilder modelBuilder, Action<EntityTypeConfiguration<TEntityType>> configurations)
            where TEntityType : class
        {
            if (modelBuilder == null) throw new ArgumentNullException("modelBuilder");
            if (configurations == null) throw new ArgumentNullException("configurations");

            var entityTypeConfiguration = modelBuilder.Entity<TEntityType>();
            configurations(entityTypeConfiguration);
            return entityTypeConfiguration;
        }

        /// <summary>
        /// Sets up a new entity in the given model and applies the configuration lambda to it
        /// </summary>
        /// <param name="modelBuilder">The model builder to be used</param>
        /// <param name="configurations">The lambda which applies the configuration</param>
        /// <param name="tableName">The table name</param>
        /// <param name="schemaName">If specified, the schema name</param>
        /// <typeparam name="TEntityType"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeConfiguration<TEntityType> Entity<TEntityType>(
            this DbModelBuilder modelBuilder, Action<EntityTypeConfiguration<TEntityType>> configurations,
            string tableName, string schemaName = null)
            where TEntityType : class
        {
            if (tableName == null) throw new ArgumentNullException("tableName");

            var entityTypeConfiguration = modelBuilder.Entity(configurations);
            if (schemaName == null)
                entityTypeConfiguration.ToTable(tableName);
            else
                entityTypeConfiguration.ToTable(tableName, schemaName);

            return entityTypeConfiguration;
        }

        #endregion

        #region Mappings

        #region Created Meta

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="maxLength">
        /// The max length for the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> property. By default <see cref="DefaultMaxLength"/> will be used.
        /// </param>
        /// <param name="propertyCreatedOnNeedsIndex">
        /// Does <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapCreatedMeta<T>(
            this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength, bool propertyCreatedOnNeedsIndex = DefaultPropertyNeedsIndex) 
            where T : class, IHaveCreatedMeta<string>
        {
            var propertyConfiguration = cfg.Property(e => e.CreatedOn).IsRequired();
            if (propertyCreatedOnNeedsIndex)
                propertyConfiguration.AddIndex();
            cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(maxLength);

            return cfg;
        }

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="mapping">
        /// Optional extra mapping for the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> property.
        /// May be used to map the inverse relation.
        /// </param>
        /// <param name="propertyCreatedOnNeedsIndex">
        /// Does <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TCreatedBy">The type of the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> property</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapCreatedMeta<T,TCreatedBy>(
            this EntityTypeConfiguration<T> cfg, Action<RequiredNavigationPropertyConfiguration<T, TCreatedBy>> mapping = null, bool propertyCreatedOnNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveCreatedMeta<TCreatedBy>
            where TCreatedBy : class 
        {
            var propertyCreatedOnConfiguration = cfg.Property(e => e.CreatedOn).IsRequired();
            if (propertyCreatedOnNeedsIndex)
                propertyCreatedOnConfiguration.AddIndex();
            var propertyConfiguration = cfg.HasRequired(e => e.CreatedBy);
            if (mapping != null)
                mapping(propertyConfiguration);

            return cfg;
        }

        #endregion

        #region Updated Meta

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="maxLength">
        /// The max length for the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property. By default <see cref="DefaultMaxLength"/> will be used.
        /// </param>
        /// <param name="propertyUpdatedOnNeedsIndex">
        /// Does <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapUpdatedMeta<T>(
            this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength, bool propertyUpdatedOnNeedsIndex = DefaultPropertyNeedsIndex) 
            where T : class, IHaveUpdatedMeta<string>
        {
            var propertyConfiguration = cfg.Property(e => e.UpdatedOn).IsRequired();
            if (propertyUpdatedOnNeedsIndex)
                propertyConfiguration.AddIndex();
            cfg.Property(e => e.UpdatedBy).IsRequired().HasMaxLength(maxLength);

            return cfg;
        }

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="mapping">
        /// Optional extra mapping for the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property. 
        /// May be used to map the inverse relation.
        /// </param>
        /// <param name="propertyUpdatedOnNeedsIndex">
        /// Does <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TUpdatedBy">The type for the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property.</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapUpdatedMeta<T, TUpdatedBy>(
            this EntityTypeConfiguration<T> cfg, Action<RequiredNavigationPropertyConfiguration<T, TUpdatedBy>> mapping = null, bool propertyUpdatedOnNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveUpdatedMeta<TUpdatedBy>
            where TUpdatedBy : class
        {
            var propertyUpdatedOnConfiguration = cfg.Property(e => e.UpdatedOn).IsRequired();
            if (propertyUpdatedOnNeedsIndex)
                propertyUpdatedOnConfiguration.AddIndex();
            var propertyConfiguration = cfg.HasRequired(e => e.UpdatedBy);
            if (mapping != null)
                mapping(propertyConfiguration);

            return cfg;
        }

        #endregion

        #region Deleted Meta

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="maxLength">
        /// The max length for the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> property. By default <see cref="DefaultMaxLength"/> will be used.
        /// </param>
        /// <param name="propertyDeletedOnNeedsIndex">
        /// Does <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapDeletedMeta<T>(
            this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength, bool propertyDeletedOnNeedsIndex = DefaultPropertyNeedsIndex) 
            where T : class, IHaveDeletedMeta<string>
        {
            var propertyConfiguration = cfg.Property(e => e.DeletedOn).IsOptional();
            if (propertyDeletedOnNeedsIndex)
                propertyConfiguration.AddIndex();
            cfg.Property(e => e.DeletedBy).IsOptional().HasMaxLength(maxLength);

            return cfg;
        }

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="mapping">
        /// Optional extra mapping for the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> property. 
        /// May be used to map the inverse relation.
        /// </param>
        /// <param name="propertyDeletedOnNeedsIndex">
        /// Does <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TDeletedBy">The type for the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> property.</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapDeletedMeta<T, TDeletedBy>(
            this EntityTypeConfiguration<T> cfg, Action<OptionalNavigationPropertyConfiguration<T, TDeletedBy>> mapping = null, bool propertyDeletedOnNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveDeletedMeta<TDeletedBy>
            where TDeletedBy : class
        {
            var propertyDeletedOnConfiguration = cfg.Property(e => e.DeletedOn).IsOptional();
            if (propertyDeletedOnNeedsIndex)
                propertyDeletedOnConfiguration.AddIndex();
            var propertyConfiguration = cfg.HasOptional(e => e.DeletedBy);
            if (mapping != null)
                mapping(propertyConfiguration);

            return cfg;
        }

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveSoftDelete"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="propertyDeletedOnNeedsIndex">
        /// Does <see cref="IHaveSoftDelete.Deleted"/> needs an index? By default <see cref="DefaultPropertyNeedsIndex"/> will be used.
        /// </param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapSoftDeleteMeta<T>(this EntityTypeConfiguration<T> cfg, bool propertyDeletedOnNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveSoftDelete
        {
            var propertyConfiguration = cfg.Property(e => e.Deleted).IsRequired();
            if (propertyDeletedOnNeedsIndex)
                propertyConfiguration.AddIndex();

            return cfg;
        }

        #endregion

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveVersionAsByteArray"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapByteArrayVersion<T>(this EntityTypeConfiguration<T> cfg) 
            where T : class, IHaveVersionAsByteArray
        {
            cfg.Property(e => e.Version).IsRequired().IsRowVersion();

            return cfg;
        }

        #endregion

        #region Indexes

        /// <summary>
        /// Adds an index to the given <see cref="DateTimePropertyConfiguration"/>
        /// </summary>
        /// <param name="cfg">The property configuration</param>
        /// <param name="name">The name of the index if needed</param>
        /// <param name="order">The index order</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static DateTimePropertyConfiguration AddIndex(this DateTimePropertyConfiguration cfg, string name = null, int? order = null)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");

            var ia = name == null
                ? new IndexAttribute()
                : (order == null
                    ? new IndexAttribute(name)
                    : new IndexAttribute(name, order.Value));
            return cfg.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(ia));
        }

        /// <summary>
        /// Adds an index to the given <see cref="PrimitivePropertyConfiguration"/>
        /// </summary>
        /// <param name="cfg">The property configuration</param>
        /// <param name="name">The name of the index if needed</param>
        /// <param name="order">The index order</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static PrimitivePropertyConfiguration AddIndex(this PrimitivePropertyConfiguration cfg, string name = null, int? order = null)
        {
            if (cfg == null) throw new ArgumentNullException("cfg");

            var ia = name == null
                ? new IndexAttribute()
                : (order == null
                    ? new IndexAttribute(name)
                    : new IndexAttribute(name, order.Value));
            return cfg.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(ia));
        }

        #endregion
    }
}
