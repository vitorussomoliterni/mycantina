namespace mycantina.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateModifiedReviewAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "DateModified");
        }
    }
}
