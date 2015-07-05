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
