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
namespace SimplePersistence.Model
{
    using System;

    /// <summary>
    /// Represents an entity that has an unique identifier, updated metadata and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TUpdatedBy">The updated by type</typeparam>
    public abstract class EntityWithUpdatedMetaAndVersionAsByteArray<TIdentity, TUpdatedBy>
        : Entity<TIdentity>, IHaveUpdatedMeta<TUpdatedBy>, IHaveVersionAsByteArray 
        where TIdentity : IEquatable<TIdentity>
    {
        private DateTimeOffset _updatedOn;

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was last updated
        /// </summary>
        public virtual DateTimeOffset UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        public virtual TUpdatedBy UpdatedBy { get; set; }

        /// <summary>
        /// The entity version
        /// </summary>
        public virtual byte[] Version { get; set; }

        /// <summary>
        /// Creates a new instance and sets the <see cref="UpdatedOn"/>
        /// to <see cref="DateTimeOffset.Now"/>
        /// </summary>
        protected EntityWithUpdatedMetaAndVersionAsByteArray()
        {
            _updatedOn = DateTimeOffset.Now;
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, updated metadata and version info, 
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveUpdatedMeta{T}.UpdatedBy"/> and 
    /// a byte[] for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithUpdatedMetaAndVersionAsByteArray<TIdentity>
        : EntityWithUpdatedMetaAndVersionAsByteArray<TIdentity, string>, IHaveUpdatedMeta 
        where TIdentity : IEquatable<TIdentity>
    {

    }
}