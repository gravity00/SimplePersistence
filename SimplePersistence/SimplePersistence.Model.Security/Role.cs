namespace SimplePersistence.Model.Security
{
    /// <summary>
    /// Represents a role
    /// </summary>
    public class Role : Entity<string>, IRole
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
}