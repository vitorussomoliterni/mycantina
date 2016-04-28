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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GrapeVariety_Bottle",
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
                "dbo.Reviews",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        BottleId = c.Int(nullable: false),
                        Text = c.String(maxLength: 4000),
                        Rating = c.Int(nullable: false),
                        DatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.BottleId })
                .ForeignKey("dbo.Bottles", t => t.BottleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BottleId);
            
            CreateTable(
                "dbo.User_Bottle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
                .ForeignKey("dbo.WineFormats", t => t.WineFormatId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BottleId)
                .Index(t => t.WineFormatId);
            
            CreateTable(
                "dbo.WineFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Bottle", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.User_Bottle", "WineFormatId", "dbo.WineFormats");
            DropForeignKey("dbo.User_Bottle", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.Reviews", "BottleId", "dbo.Bottles");
            DropForeignKey("dbo.GrapeVariety_Bottle", "GrapeVarietyId", "dbo.GrapeVarieties");
            DropForeignKey("dbo.GrapeVariety_Bottle", "BottleId", "dbo.Bottles");
            DropIndex("dbo.User_Bottle", new[] { "WineFormatId" });
            DropIndex("dbo.User_Bottle", new[] { "BottleId" });
            DropIndex("dbo.User_Bottle", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "BottleId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.GrapeVariety_Bottle", new[] { "BottleId" });
            DropIndex("dbo.GrapeVariety_Bottle", new[] { "GrapeVarietyId" });
            DropTable("dbo.Users");
            DropTable("dbo.WineFormats");
            DropTable("dbo.User_Bottle");
            DropTable("dbo.Reviews");
            DropTable("dbo.GrapeVarieties");
            DropTable("dbo.GrapeVariety_Bottle");
            DropTable("dbo.Bottles");
        }
    }
}
