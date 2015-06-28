using System.Collections.Generic;
using SimplePersistence.Model;

namespace SimplePersistence.Example.WebApi.Models.Logging
{
    public class Level : EntityWithAllMetaAndVersionAsLong<string>
    {
        private ICollection<Log> _logs;

        public virtual string Description { get; set; }

        public virtual ICollection<Log> Logs
        {
            get { return _logs; }
            protected set { _logs = value; }
        }

        public Level()
        {
            _logs = new HashSet<Log>();
        }
    }
}