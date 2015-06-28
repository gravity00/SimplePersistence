using System;
using System.Data.Entity.Migrations;
using SimplePersistence.Example.WebApi.UoW.EF.Mapping;
using SimplePersistence.Example.WebApi.UoW.EF.Mapping.Seed;

namespace SimplePersistence.Example.WebApi.UoW.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApiDbContext context)
        {
            const string migrationUser = "script.migration";
            var seedExecutedOn = DateTimeOffset.Now;

            context.SeedLoggingData(seedExecutedOn, migrationUser);
        }
    }
}
