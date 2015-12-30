using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using SimplePersistence.Example.Console.Models.Logging;
using SimplePersistence.Model;

namespace SimplePersistence.Example.Console.UoW.EF.Mapping
{
    public class ConsoleDbContext : DbContext
    {
        public ConsoleDbContext()
            : this("name=ConsoleDbContext")
        {
            
        }

        public ConsoleDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(128);

                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);
                cfg.HasMany(e => e.Logs).WithRequired(e => e.Application);

                cfg.Property(e => e.CreatedOn).IsRequired();
                cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(128);
            }, "Applications");

            modelBuilder.Entity<Level>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasMaxLength(64);

                cfg.Property(e => e.Description).IsRequired().HasMaxLength(512);
                cfg.HasMany(e => e.Logs).WithRequired(e => e.Level);

                cfg.Property(e => e.CreatedOn).IsRequired();
                cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(128);
            }, "Levels");

            modelBuilder.Entity<Log>(cfg =>
            {
                cfg.HasKey(e => e.Id)
                    .Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                cfg.HasRequired(e => e.Level).WithMany(e => e.Logs).Map(m => m.MapKey("Level"));
                cfg.HasRequired(e => e.Application).WithMany(e => e.Logs).Map(m => m.MapKey("Application"));
                cfg.Property(e => e.Logger).IsRequired().HasMaxLength(256);
                cfg.Property(e => e.Thread).IsRequired().HasMaxLength(64);
                cfg.Property(e => e.Message).IsRequired();
                cfg.Property(e => e.Exception).IsOptional();

                cfg.Property(e => e.CreatedOn).IsRequired();
                cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(128);
            }, "Logs");
        }
    }


    /// <summary>
    /// Extension methods for code first configuration
    /// </summary>
    public static class EntityTypeConfigurationExtensions
    {
        public static EntityTypeConfiguration<TEntityType> Entity<TEntityType>(
            this DbModelBuilder modelBuilder, Action<EntityTypeConfiguration<TEntityType>> configurations, string tableName = null, string schemaName = null)
            where TEntityType : class
        {
            var entityTypeConfiguration = modelBuilder.Entity<TEntityType>();
            if (tableName != null)
                if (schemaName == null)
                    entityTypeConfiguration.ToTable(tableName);
                else
                    entityTypeConfiguration.ToTable(tableName, schemaName);

            configurations(entityTypeConfiguration);
            return entityTypeConfiguration;
        }

        public static EntityTypeConfiguration<TEntityType> MapCreatedMeta<TEntityType>(this EntityTypeConfiguration<TEntityType> entityTypeConfiguration)
            where TEntityType : class , IHaveCreatedMeta<string>
        {
            entityTypeConfiguration.Property(e => e.CreatedOn).IsRequired();
            entityTypeConfiguration.Property(e => e.CreatedBy).IsRequired().HasMaxLength(128);

            return entityTypeConfiguration;
        }
    }
}
