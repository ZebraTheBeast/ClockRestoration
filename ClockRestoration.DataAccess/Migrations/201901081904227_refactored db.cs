namespace ClockRestoration.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoreddb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClockPhoto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId);
            
            DropColumn("dbo.Order", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "ImageUrl", c => c.String());
            DropForeignKey("dbo.ClockPhoto", "OrderId", "dbo.Order");
            DropIndex("dbo.ClockPhoto", new[] { "OrderId" });
            DropTable("dbo.ClockPhoto");
        }
    }
}
