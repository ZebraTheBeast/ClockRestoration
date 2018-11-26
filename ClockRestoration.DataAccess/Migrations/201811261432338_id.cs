namespace ClockRestoration.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Order", new[] { "User_Id" });
            DropColumn("dbo.Order", "UserId");
            RenameColumn(table: "dbo.Order", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Order", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Order", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "UserId" });
            AlterColumn("dbo.Order", "UserId", c => c.Long(nullable: false));
            RenameColumn(table: "dbo.Order", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Order", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Order", "User_Id");
        }
    }
}
