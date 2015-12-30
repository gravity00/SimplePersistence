using System;
using SimplePersistence.Model;

namespace SimplePersistence.Example.Console.Models.Logging
{
    public class Log : Entity<long>
    {
        public virtual Level Level { get; set; }
        public virtual Application Application { get; set; }
        public string Logger { get; set; }
        public string Thread { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public Log()
        {
            CreatedOn = DateTime.Now;
        }
    }
}