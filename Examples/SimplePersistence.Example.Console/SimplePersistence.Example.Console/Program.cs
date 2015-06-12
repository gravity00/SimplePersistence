using System;
using System.Data.Entity;
using System.IO;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.Example.Console.UoW.EF;
using SimplePersistence.Example.Console.UoW.EF.Mapping;
using SimplePersistence.Example.Console.UoW.EF.Migrations;

namespace SimplePersistence.Example.Console
{
    public class Program
    {
        #region Application startup

        public static void Main(string[] args)
        {
            System.Console.WriteLine("Application started...");
            try
            {
                var dataDirectory =
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "SimplePersistence\\SimplePersistence.Example.Console\\data");
                if (!Directory.Exists(dataDirectory))
                    Directory.CreateDirectory(dataDirectory);
                AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
                System.Console.WriteLine("All data files will be stored in " + dataDirectory);

                Database.SetInitializer(new Configuration());

                new WindsorContainer()
                    .Register(
                        Component.For<Program>().LifestyleSingleton())
                    .Resolve<Program>()
                    .Run(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("An unhandled exception has occured");
                System.Console.WriteLine(e);
            }
            System.Console.WriteLine("Application ended. Press <enter> to exit...");
            System.Console.ReadLine();
        }

        #endregion

        public void Run(string[] args)
        {
            ExampleWithoutIoC();
        }

        private void ExampleWithoutIoC()
        {
            using (var uow = new ConsoleUnitOfWork(new ConsoleDbContext()))
            {
                uow.Begin();

                var application = uow.Logging.Applications.GetById("SimplePersistence.Example.Console");
                var levels = new[]
                {
                    uow.Logging.Levels.GetById("FATAL"),
                    uow.Logging.Levels.GetById("ERROR"),
                    uow.Logging.Levels.GetById("WARN"),
                    uow.Logging.Levels.GetById("INFO"),
                    uow.Logging.Levels.GetById("DEBUG")
                };

                var randomSeed = new Random();

                var exception =
                    new ArgumentOutOfRangeException(
                        "This is a dummy ArgumentOutOfRangeException",
                        new Exception("This is the dummy ArgumentOutOfRangeException.InnerException"));

                const int logsToInsert = 1000;
                var logs = new Log[logsToInsert];
                for (var i = 0; i < logsToInsert; i++)
                {
                    var levelIdx = randomSeed.Next(0, 4);
                    var level = levels[levelIdx];

                    logs[i] =
                        new Log
                        {
                            Level = level,
                            Application = application,
                            CreatedOn = DateTimeOffset.Now,
                            CreatedBy = "some-user",
                            Logger = "SimplePersistence.Example.Console.Program",
                            Exception = levelIdx == 0 || levelIdx == 1 ? exception.ToString() : null,
                            Message = "This is a " + level + " log message",
                            Thread = "thread-" + i
                        };
                }

                uow.Logging.Logs.Add(logs);

                uow.Commit();
            }
            System.Console.WriteLine();
        }

        private void ExampleWithIoC()
        {
            //  Do nothing for now
        }
    }
}
