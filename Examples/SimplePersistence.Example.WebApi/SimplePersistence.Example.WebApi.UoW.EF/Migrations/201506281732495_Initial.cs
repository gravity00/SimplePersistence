namespace SimplePersistence.Example.WebApi.UoW.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Logging.Applications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 512),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                        DeletedOn = c.DateTimeOffset(precision: 7),
                        DeletedBy = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Logging.Logs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Logger = c.String(nullable: false, maxLength: 256),
                        Thread = c.String(nullable: false, maxLength: 64),
                        Message = c.String(nullable: false),
                        Exception = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        LevelId = c.String(nullable: false, maxLength: 128),
                        ApplicationId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Logging.Levels", t => t.LevelId, cascadeDelete: true)
                .ForeignKey("Logging.Applications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.LevelId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "Logging.Levels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false, maxLength: 512),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                        DeletedOn = c.DateTimeOffset(precision: 7),
                        DeletedBy = c.String(maxLength: 128),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Logging.Logs", "ApplicationId", "Logging.Applications");
            DropForeignKey("Logging.Logs", "LevelId", "Logging.Levels");
            DropIndex("Logging.Logs", new[] { "ApplicationId" });
            DropIndex("Logging.Logs", new[] { "LevelId" });
            DropTable("Logging.Levels");
            DropTable("Logging.Logs");
            DropTable("Logging.Applications");
        }
    }
}
