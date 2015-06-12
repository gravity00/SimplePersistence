using SimplePersistence.Model;

namespace SimplePersistence.Example.Console.Models.Logging
{
    public class Log : EntityWithCreatedMeta<long>
    {
        public virtual Level Level { get; set; }
        public virtual Application Application { get; set; }
        public string Logger { get; set; }
        public string Thread { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}