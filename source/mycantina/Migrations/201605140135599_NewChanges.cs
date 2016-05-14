namespace mycantina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConsumerBottles", "Bottle_Id", "dbo.Bottles");
            DropForeignKey("dbo.ConsumerBottles", "Consumer_Id", "dbo.Consumers");
            DropIndex("dbo.ConsumerBottles", new[] { "Bottle_Id" });
            DropIndex("dbo.ConsumerBottles", new[] { "Consumer_Id" });
            RenameColumn(table: "dbo.ConsumerBottles", name: "Bottle_Id", newName: "BottleId");
            RenameColumn(table: "dbo.ConsumerBottles", name: "Consumer_Id", newName: "ConsumerId");
            AlterColumn("dbo.ConsumerBottles", "BottleId", c => c.Int(nullable: false));
            AlterColumn("dbo.ConsumerBottles", "ConsumerId", c => c.Int(nullable: false));
            CreateIndex("dbo.ConsumerBottles", "ConsumerId");
            CreateIndex("dbo.ConsumerBottles", "BottleId");
            AddForeignKey("dbo.ConsumerBottles", "BottleId", "dbo.Bottles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConsumerBottles", "ConsumerId", "dbo.Consumers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConsumerBottles", "ConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.ConsumerBottles", "BottleId", "dbo.Bottles");
            DropIndex("dbo.ConsumerBottles", new[] { "BottleId" });
            DropIndex("dbo.ConsumerBottles", new[] { "ConsumerId" });
            AlterColumn("dbo.ConsumerBottles", "ConsumerId", c => c.Int());
            AlterColumn("dbo.ConsumerBottles", "BottleId", c => c.Int());
            RenameColumn(table: "dbo.ConsumerBottles", name: "ConsumerId", newName: "Consumer_Id");
            RenameColumn(table: "dbo.ConsumerBottles", name: "BottleId", newName: "Bottle_Id");
            CreateIndex("dbo.ConsumerBottles", "Consumer_Id");
            CreateIndex("dbo.ConsumerBottles", "Bottle_Id");
            AddForeignKey("dbo.ConsumerBottles", "Consumer_Id", "dbo.Consumers", "Id");
            AddForeignKey("dbo.ConsumerBottles", "Bottle_Id", "dbo.Bottles", "Id");
        }
    }
}
