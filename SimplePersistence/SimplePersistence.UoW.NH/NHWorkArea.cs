using NHibernate;

namespace SimplePersistence.UoW.NH
{
    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the NHibernate
    /// </summary>
    public abstract class NHWorkArea : INHWorkArea
    {
        private readonly ISession _session;

        /// <summary>
        /// Creates a new work area that will use the given database session
        /// </summary>
        /// <param name="session">The database session</param>
        protected NHWorkArea(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// The database session object
        /// </summary>
        public ISession Session { get { return _session; } }
    }
}