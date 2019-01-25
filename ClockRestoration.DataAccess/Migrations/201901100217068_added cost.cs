namespace ClockRestoration.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "Cost");
        }
    }
}
