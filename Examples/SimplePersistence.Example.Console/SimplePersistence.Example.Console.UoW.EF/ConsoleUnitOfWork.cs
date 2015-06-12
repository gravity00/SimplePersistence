using SimplePersistence.Example.Console.UoW.Area;
using SimplePersistence.Example.Console.UoW.EF.Area;
using SimplePersistence.Example.Console.UoW.EF.Mapping;
using SimplePersistence.UoW.EF;

namespace SimplePersistence.Example.Console.UoW.EF
{
    public class ConsoleUnitOfWork : EFUnitOfWork, IConsoleUnitOfWork
    {
        private readonly ILoggingWorkArea _logging;

        public ConsoleUnitOfWork(ConsoleDbContext context)
            : base(context)
        {
            _logging = new LoggingWorkArea(context);
        }

        public ILoggingWorkArea Logging
        {
            get { return _logging; }
        }
    }
}