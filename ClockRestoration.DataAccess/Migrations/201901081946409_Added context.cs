namespace ClockRestoration.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcontext : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClockPhoto", newName: "OrderClockPhoto");
            CreateTable(
                "dbo.Gallery",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        MainImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GalleryPhoto",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        GalleryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Gallery", t => t.GalleryId)
                .Index(t => t.GalleryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GalleryPhoto", "GalleryId", "dbo.Gallery");
            DropIndex("dbo.GalleryPhoto", new[] { "GalleryId" });
            DropTable("dbo.GalleryPhoto");
            DropTable("dbo.Gallery");
            RenameTable(name: "dbo.OrderClockPhoto", newName: "ClockPhoto");
        }
    }
}
