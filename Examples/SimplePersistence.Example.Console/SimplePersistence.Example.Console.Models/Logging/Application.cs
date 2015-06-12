using System.Collections.Generic;
using SimplePersistence.Model;

namespace SimplePersistence.Example.Console.Models.Logging
{
    public class Application : EntityWithCreatedMeta<string>
    {
        private ICollection<Log> _logs;

        public string Description { get; set; }

        public virtual ICollection<Log> Logs
        {
            get { return _logs; }
            protected set { _logs = value; }
        }

        public Application()
        {
            _logs = new HashSet<Log>();
        }
    }
}
