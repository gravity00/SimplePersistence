using System;
using System.Linq;
using SimplePersistence.Example.WebApi.Models.Logging;

namespace SimplePersistence.Example.WebApi.UoW.EF.Mapping.Seed
{
    public static class LoggingSeed
    {
        public static ApiDbContext SeedLoggingData(this ApiDbContext context, DateTimeOffset seedExecutedOn, string seedUser)
        {
            return context
                .SeedLoggingLevels(seedExecutedOn, seedUser)
                .SeedLoggingApplications(seedExecutedOn, seedUser);
        }

        private static ApiDbContext SeedLoggingLevels(this ApiDbContext context, DateTimeOffset seedExecutedOn, string seedUser)
        {
            var levelsToSeed = new[]
            {
                new Level
                {
                    Id = "DEBUG",
                    Description = "Debug",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                },
                new Level
                {
                    Id = "INFO",
                    Description = "Information",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                },
                new Level
                {
                    Id = "WARN",
                    Description = "Warning",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                },
                new Level
                {
                    Id = "ERROR",
                    Description = "Error",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                },
                new Level
                {
                    Id = "FATAL",
                    Description = "Fatal",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                }
            };

            var existingLevels = context.Levels.ToArray();
            foreach (var levelToSeed in levelsToSeed)
            {
                var existingLevel = existingLevels.FirstOrDefault(e => e.Id == levelToSeed.Id);
                if (existingLevel != null)
                {
                    existingLevel.Description = levelToSeed.Description;
                    existingLevel.UpdatedOn = seedExecutedOn;
                    existingLevel.UpdatedBy = seedUser;
                }
                else
                {
                    context.Levels.Add(levelToSeed);
                }
            }

            return context;
        }

        private static ApiDbContext SeedLoggingApplications(this ApiDbContext context, DateTimeOffset seedExecutedOn, string seedUser)
        {
            var applicationsToSeed = new[]
            {
                new Application
                {
                    Id = "SimplePersistence.Example.WebApi",
                    Description = "An example WebAPI application from SimplePersistence",
                    CreatedOn = seedExecutedOn,
                    CreatedBy = seedUser,
                    UpdatedOn = seedExecutedOn,
                    UpdatedBy = seedUser
                }
            };

            var existingApplications = context.Applications.ToArray();
            foreach (var applicationToSeed in applicationsToSeed)
            {
                var existingApplication = existingApplications.FirstOrDefault(e => e.Id == applicationToSeed.Id);
                if (existingApplication != null)
                {
                    existingApplication.Description = applicationToSeed.Description;
                    existingApplication.UpdatedOn = seedExecutedOn;
                    existingApplication.UpdatedBy = seedUser;
                }
                else
                {
                    context.Applications.Add(applicationToSeed);
                }
            }

            return context;
        }
    }
}
