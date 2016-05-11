namespace mycantina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BottleEntityModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bottles", "GrapeVarietyId", c => c.Int(nullable: false));
            DropColumn("dbo.Bottles", "GrapeVariety");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bottles", "GrapeVariety", c => c.String());
            DropColumn("dbo.Bottles", "GrapeVarietyId");
        }
    }
}
