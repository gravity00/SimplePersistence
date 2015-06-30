namespace SimplePersistence.Model.Helper
{
    using System;

    /// <summary>
    /// Models extension methods
    /// </summary>
    public static class ModelsExtensions
    {
        /// <summary>
        /// Fills all the created metadata of a given <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">The by of whom created the entity</param>
        /// <param name="on">The <see cref="DateTimeOffset"/> when it was created. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The received entity after changes</returns>
        public static T SetInitialMeta<T>(this T entity, string @by = null, DateTimeOffset? @on = null) 
            where T : IHaveCreatedMeta<string>
        {
            entity.CreatedOn = @on ?? DateTimeOffset.Now;
            entity.CreatedBy = @by;
            return entity;
        }

        /// <summary>
        /// Fills all the created metadata of a given <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="username">The by of whom created the entity</param>
        /// <param name="updatedOn">The <see cref="DateTimeOffset"/> when it was created. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The received entity after changes</returns>
        public static T FillUpdatedMeta<T>(this T entity, string username = null, DateTimeOffset? updatedOn = null) where T : IHaveUpdatedMeta<string>
        {
            entity.UpdatedOn = updatedOn ?? DateTimeOffset.Now;
            entity.UpdatedBy = username;
            return entity;
        }
    }
}
