namespace mycantina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bottles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Year = c.Int(nullable: false),
                        Producer = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 4000),
                        MinPrice = c.Decimal(nullable: false, storeType: "money"),
                        MaxPrice = c.Decimal(nullable: false, storeType: "money"),
                        GrapeVarietyId = c.Int(nullable: false),
                        WineTypeId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.WineTypes", t => t.WineTypeId, cascadeDelete: true)
                .Index(t => t.WineTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.ConsumerBottles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConsumerId = c.Int(nullable: false),
                        BottleId = c.Int(nullable: false),
                        WineFormatId = c.Int(nullable: false),
                        DateAcquired = c.DateTime(),
                        DateOpened = c.DateTime(),
                        QtyOwned = c.Int(nullable: false),
                        Owned = c.Boolean(nullable: false),
                        PricePaid = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bottles", t => t.BottleId, cascadeDelete: true)
                .ForeignKey("dbo.Consumers", t => t.ConsumerId, cascadeDelete: true)
                .ForeignKey("dbo.WineFormats", t => t.WineFormatId, cascadeDelete: true)
                .Index(t => t.ConsumerId)
                .Index(t => t.BottleId)
                .Index(t => t.WineFormatId);
            
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        MiddleNames = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ConsumerId = c.Int(nullable: false),
                        BottleId = c.Int(nullable: false),
                        Text = c.String(maxLength: 4000),
                        Rating = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ConsumerId, t.BottleId })
                .ForeignKey("dbo.Bottles", t => t.BottleId, cascadeDelete: true)
                .ForeignKey("dbo.Consumers", t => t.ConsumerId, cascadeDelete: true)
                .Index(t => t.ConsumerId)
                .Index(t => t.BottleId);
            
            CreateTable(
                "dbo.WineFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.GrapeVarietyBottles",
                c => new
                    {
                        GrapeVarietyId = c.Int(nullable: false),
                        BottleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrapeVarietyId, t.BottleId })
                .ForeignKey("dbo.Bottles", t => t.BottleId, cascadeDelete: true)
                .ForeignKey("dbo.GrapeVarieties", t => t.GrapeVarietyId, cascadeDelete: true)
                .Index(t => t.GrapeVarietyId)
                .Index(t => t.BottleId);
            
            CreateTable(
                "dbo.GrapeVarieties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bottles", "WineTypeId", "dbo.WineTypes");
            DropForeignKey("dbo.GrapeVarietyBottles", "GrapeVarietyId", "dbo.GrapeVarieties");
            DropForeignKey("dbo.GrapeVarietyBottles", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Bottles", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Bottles", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ConsumerBottles", "WineFormatId", "dbo.WineFormats");
            DropForeignKey("dbo.Reviews", "ConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.Reviews", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.ConsumerBottles", "ConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.ConsumerBottles", "BottleId", "dbo.Bottles");
            DropIndex("dbo.GrapeVarietyBottles", new[] { "BottleId" });
            DropIndex("dbo.GrapeVarietyBottles", new[] { "GrapeVarietyId" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Reviews", new[] { "BottleId" });
            DropIndex("dbo.Reviews", new[] { "ConsumerId" });
            DropIndex("dbo.ConsumerBottles", new[] { "WineFormatId" });
            DropIndex("dbo.ConsumerBottles", new[] { "BottleId" });
            DropIndex("dbo.ConsumerBottles", new[] { "ConsumerId" });
            DropIndex("dbo.Bottles", new[] { "RegionId" });
            DropIndex("dbo.Bottles", new[] { "CountryId" });
            DropIndex("dbo.Bottles", new[] { "WineTypeId" });
            DropTable("dbo.WineTypes");
            DropTable("dbo.GrapeVarieties");
            DropTable("dbo.GrapeVarietyBottles");
            DropTable("dbo.Regions");
            DropTable("dbo.Countries");
            DropTable("dbo.WineFormats");
            DropTable("dbo.Reviews");
            DropTable("dbo.Consumers");
            DropTable("dbo.ConsumerBottles");
            DropTable("dbo.Bottles");
        }
    }
}
