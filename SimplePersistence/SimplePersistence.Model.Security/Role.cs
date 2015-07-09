namespace SimplePersistence.Model.Security
{
    using System;

    /// <summary>
    /// Represents a role
    /// </summary>
    /// <typeparam name="TIdentity">The identity type</typeparam>
    public class Role<TIdentity> : Entity<TIdentity>, IRole<TIdentity> 
        where TIdentity : IEquatable<TIdentity>
    {
        /// <summary>
        /// The role name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The role description
        /// </summary>
        public virtual string Description { get; set; }
    }

    /// <summary>
    /// Represents a role
    /// </summary>
    public class Role : Role<string>
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Role()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates a new role with the given name
        /// </summary>
        /// <param name="name">The name for this role</param>
        /// <param name="description">The role name</param>
        public Role(string name, string description = null) : this()
        {
            Name = name;
            Description = description;
        }
    }
}