using SimplePersistence.Model;

namespace SimplePersistence.Example.WebApi.Models.Logging
{
    public class Log : EntityWithCreatedMeta<long>
    {
        public virtual Level Level { get; set; }
        public virtual Application Application { get; set; }
        public virtual string Logger { get; set; }
        public virtual string Thread { get; set; }
        public virtual string Message { get; set; }
        public virtual string Exception { get; set; }
    }
}