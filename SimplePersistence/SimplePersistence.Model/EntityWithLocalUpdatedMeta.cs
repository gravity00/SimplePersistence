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
    /// Represents an entity that has an unique identifier and local updated metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TUpdatedBy">The created by type</typeparam>
    public abstract class EntityWithLocalUpdatedMeta<TIdentity, TUpdatedBy>
        : Entity<TIdentity>, IHaveLocalUpdatedMeta<TUpdatedBy>
        where TIdentity : IEquatable<TIdentity>
    {
        private DateTime _updatedOn;

        /// <summary>
        /// The <see cref="DateTime"/> when it was created
        /// </summary>
        public virtual DateTime UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        public virtual TUpdatedBy UpdatedBy { get; set; }
        
        /// <summary>
        /// Creates a new instance and sets the <see cref="UpdatedOn"/>
        /// to <see cref="DateTime.Now"/>
        /// </summary>
        protected EntityWithLocalUpdatedMeta()
        {
            _updatedOn = DateTime.Now;
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier and updated metadata, 
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveLocalUpdatedMeta{T}.UpdatedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalUpdatedMeta<TIdentity>
        : EntityWithLocalUpdatedMeta<TIdentity, string>, IHaveLocalUpdatedMeta
        where TIdentity : IEquatable<TIdentity>
    {
        
    }
}
