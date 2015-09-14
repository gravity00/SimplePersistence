using System;

namespace SimplePersistence.Model
{
    /// <summary>
    /// Represents an entity that has an unique identifier and local created metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TCreatedBy">The created by type</typeparam>
    public abstract class EntityWithLocalCreatedMeta<TIdentity, TCreatedBy>
        : Entity<TIdentity>, IHaveLocalCreatedMeta<TCreatedBy>
        where TIdentity : IEquatable<TIdentity>
    {
        private DateTime _createdOn;

        /// <summary>
        /// The <see cref="DateTime"/> when it was created
        /// </summary>
        public virtual DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        public virtual TCreatedBy CreatedBy { get; set; }
        
        /// <summary>
        /// Creates a new instance and sets the <see cref="CreatedOn"/>
        /// to <see cref="DateTime.Now"/>
        /// </summary>
        protected EntityWithLocalCreatedMeta()
        {
            _createdOn = DateTime.Now;
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier and created metadata, 
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveLocalCreatedMeta{T}.CreatedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalCreatedMeta<TIdentity>
        : EntityWithLocalCreatedMeta<TIdentity, string>, IHaveLocalCreatedMeta
        where TIdentity : IEquatable<TIdentity>
    {
        
    }
}
