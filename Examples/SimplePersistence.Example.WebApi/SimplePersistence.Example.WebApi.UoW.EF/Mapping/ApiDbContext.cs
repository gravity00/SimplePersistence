using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SimplePersistence.Example.WebApi.Models.Logging;
using SimplePersistence.Model.EF.Fluent;

namespace SimplePersistence.Example.WebApi.UoW.EF.Mapping
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext()
            : this("name=ApiDbContext")
        {

        }

        public ApiDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            if (nameOrConnectionString == null) throw new ArgumentNullException("nameOrConnectionString");
        }

        #region DbSets

        #region Logging

        public DbSet<Application> Applications { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Log> Logs { get; set; } 

        #endregion

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Logging

            modelBuilder.Entity<Application>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(128);

                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);
                cfg.HasMany(e => e.Logs).WithRequired(e => e.Application);

                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapByteArrayVersion();
            }, "Applications", "Logging");

            modelBuilder.Entity<Level>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(128);

                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);
                cfg.HasMany(e => e.Logs).WithRequired(e => e.Level);

                cfg.MapCreatedMeta().MapUpdatedMeta().MapDeletedMeta().MapByteArrayVersion();
            }, "Levels", "Logging");

            modelBuilder.Entity<Log>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                cfg.HasRequired(e => e.Level).WithMany(e => e.Logs).Map(m => m.MapKey("LevelId"));
                cfg.HasRequired(e => e.Application).WithMany(e => e.Logs).Map(m => m.MapKey("ApplicationId"));
                cfg.Property(e => e.Logger).IsRequired().HasMaxLength(256);
                cfg.Property(e => e.Thread).IsRequired().HasMaxLength(64);
                cfg.Property(e => e.Message).IsRequired();
                cfg.Property(e => e.Exception).IsOptional();

                cfg.MapCreatedMeta();
            }, "Logs", "Logging");

            #endregion
        }
    }
}
