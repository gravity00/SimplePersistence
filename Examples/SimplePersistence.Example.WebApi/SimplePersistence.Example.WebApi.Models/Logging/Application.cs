using System.Collections.Generic;
using SimplePersistence.Model;

namespace SimplePersistence.Example.WebApi.Models.Logging
{
    public class Application : EntityWithAllMetaAndVersionAsByteArray<string>
    {
        private ICollection<Log> _logs;

        public virtual string Description { get; set; }

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
