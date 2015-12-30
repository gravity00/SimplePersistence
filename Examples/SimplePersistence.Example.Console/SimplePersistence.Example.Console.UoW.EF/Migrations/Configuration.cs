using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.Example.Console.UoW.EF.Mapping;

namespace SimplePersistence.Example.Console.UoW.EF.Migrations
{
    public sealed class Configuration : DropCreateDatabaseIfModelChanges<ConsoleDbContext>//DbMigrationsConfiguration<ConsoleDbContext>
    {
        //public Configuration()
        //{
        //    AutomaticMigrationsEnabled = false;
        //}

        protected override void Seed(ConsoleDbContext context)
        {
            context.Applications.AddOrUpdate(
                e => e.Id,
                new Application
                {
                    Id = "SimplePersistence.Example.Console",
                    Description = "The SimplePersistence.Example.Console application",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                });

            context.Levels.AddOrUpdate(
                e => e.Id,
                new Level
                {
                    Id = "DEBUG",
                    Description = "Debug",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                },
                new Level
                {
                    Id = "Info",
                    Description = "Information",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                },
                new Level
                {
                    Id = "WARN",
                    Description = "Warning",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                },
                new Level
                {
                    Id = "ERROR",
                    Description = "Error",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                },
                new Level
                {
                    Id = "FATAL",
                    Description = "Fatal",
                    CreatedOn = DateTime.Now,
                    CreatedBy = "seed.migration"
                });
        }
    }
}
