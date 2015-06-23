namespace SimplePersistence.UoW.NH
{
    using NHibernate;

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the NHibernate
    /// </summary>
    public interface INHWorkArea : IWorkArea
    {
        /// <summary>
        /// The database session object
        /// </summary>
        ISession Session { get; }
    }
}
