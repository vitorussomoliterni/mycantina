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
                        Region = c.String(maxLength: 100),
                        Country = c.String(nullable: false, maxLength: 50, unicode: false),
                        Type = c.String(nullable: false, maxLength: 50),
                        Year = c.DateTime(nullable: false),
                        Producer = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 4000),
                        MinPrice = c.Decimal(nullable: false, storeType: "money"),
                        MaxPrice = c.Decimal(nullable: false, storeType: "money"),
                        GrapeVariety = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConsumerBottles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WineFormatId = c.Int(nullable: false),
                        DateAcquired = c.DateTime(),
                        DateOpened = c.DateTime(),
                        QtyOwned = c.Int(nullable: false),
                        Owned = c.Boolean(nullable: false),
                        PricePaid = c.Decimal(nullable: false, storeType: "money"),
                        Bottle_Id = c.Int(),
                        Consumer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bottles", t => t.Bottle_Id)
                .ForeignKey("dbo.Consumers", t => t.Consumer_Id)
                .ForeignKey("dbo.WineFormats", t => t.WineFormatId, cascadeDelete: true)
                .Index(t => t.WineFormatId)
                .Index(t => t.Bottle_Id)
                .Index(t => t.Consumer_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GrapeVarietyBottles", "GrapeVarietyId", "dbo.GrapeVarieties");
            DropForeignKey("dbo.GrapeVarietyBottles", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.ConsumerBottles", "WineFormatId", "dbo.WineFormats");
            DropForeignKey("dbo.Reviews", "ConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.Reviews", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.ConsumerBottles", "Consumer_Id", "dbo.Consumers");
            DropForeignKey("dbo.ConsumerBottles", "Bottle_Id", "dbo.Bottles");
            DropIndex("dbo.GrapeVarietyBottles", new[] { "BottleId" });
            DropIndex("dbo.GrapeVarietyBottles", new[] { "GrapeVarietyId" });
            DropIndex("dbo.Reviews", new[] { "BottleId" });
            DropIndex("dbo.Reviews", new[] { "ConsumerId" });
            DropIndex("dbo.ConsumerBottles", new[] { "Consumer_Id" });
            DropIndex("dbo.ConsumerBottles", new[] { "Bottle_Id" });
            DropIndex("dbo.ConsumerBottles", new[] { "WineFormatId" });
            DropTable("dbo.GrapeVarieties");
            DropTable("dbo.GrapeVarietyBottles");
            DropTable("dbo.WineFormats");
            DropTable("dbo.Reviews");
            DropTable("dbo.Consumers");
            DropTable("dbo.ConsumerBottles");
            DropTable("dbo.Bottles");
        }
    }
}
