using System;
using System.Collections.Generic;
using SimplePersistence.Model;

namespace SimplePersistence.Example.Console.Models.Logging
{
    public class Application : Entity<string>
    {
        private ICollection<Log> _logs;

        public string Description { get; set; }

        public virtual ICollection<Log> Logs
        {
            get { return _logs; }
            protected set { _logs = value; }
        }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public Application()
        {
            _logs = new HashSet<Log>();

            CreatedOn = DateTime.Now;
        }
    }
}
