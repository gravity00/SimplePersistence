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
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using Model;

    /// <summary>
    /// Extension methods for Entity Framework code first mappings
    /// </summary>
    public static class CodeFirstMappingExtensions
    {
        /// <summary>
        /// The default max length used in metadata fields
        /// </summary>
        public const int DefaultMaxLength = 128;

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
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapCreatedMeta<T>(this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength) 
            where T : class, IHaveCreatedMeta<string>
        {
            cfg.Property(e => e.CreatedOn).IsRequired();
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TCreatedBy">The type of the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> property</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapCreatedMeta<T,TCreatedBy>(
            this EntityTypeConfiguration<T> cfg, Action<RequiredNavigationPropertyConfiguration<T, TCreatedBy>> mapping = null)
            where T : class, IHaveCreatedMeta<TCreatedBy>
            where TCreatedBy : class 
        {
            cfg.Property(e => e.CreatedOn).IsRequired();
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapUpdatedMeta<T>(this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength) 
            where T : class, IHaveUpdatedMeta<string>
        {
            cfg.Property(e => e.UpdatedOn).IsRequired();
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TUpdatedBy">The type for the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property.</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapUpdatedMeta<T, TUpdatedBy>(
            this EntityTypeConfiguration<T> cfg, Action<RequiredNavigationPropertyConfiguration<T, TUpdatedBy>> mapping = null)
            where T : class, IHaveUpdatedMeta<TUpdatedBy>
            where TUpdatedBy : class
        {
            cfg.Property(e => e.UpdatedOn).IsRequired();
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapDeletedMeta<T>(this EntityTypeConfiguration<T> cfg, int maxLength = DefaultMaxLength) 
            where T : class, IHaveDeletedMeta<string>
        {
            cfg.Property(e => e.DeletedOn).IsOptional();
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
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TDeletedBy">The type for the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> property.</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapDeletedMeta<T, TDeletedBy>(
            this EntityTypeConfiguration<T> cfg, Action<OptionalNavigationPropertyConfiguration<T, TDeletedBy>> mapping = null)
            where T : class, IHaveDeletedMeta<TDeletedBy>
            where TDeletedBy : class
        {
            cfg.Property(e => e.DeletedOn).IsOptional();
            var propertyConfiguration = cfg.HasOptional(e => e.DeletedBy);
            if (mapping != null)
                mapping(propertyConfiguration);

            return cfg;
        }

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveSoftDelete"/>
        /// </summary>
        /// <param name="cfg">The entity configuration</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The entity configuration after changes</returns>
        public static EntityTypeConfiguration<T> MapSoftDeleteMeta<T>(this EntityTypeConfiguration<T> cfg)
            where T : class, IHaveSoftDelete
        {
            cfg.Property(e => e.Deleted).IsRequired();

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
    }
}
