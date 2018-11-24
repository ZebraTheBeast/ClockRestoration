namespace ClockRestoration.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Order", new[] { "Brand_Id" });
            DropIndex("dbo.Order", new[] { "ClockType_Id" });
            DropIndex("dbo.Order", new[] { "Delivery_Id" });
            DropIndex("dbo.Order", new[] { "Payment_Id" });
            RenameColumn(table: "dbo.Order", name: "Brand_Id", newName: "BrandId");
            RenameColumn(table: "dbo.Order", name: "ClockType_Id", newName: "ClockTypeId");
            RenameColumn(table: "dbo.Order", name: "Delivery_Id", newName: "DeliveryId");
            RenameColumn(table: "dbo.Order", name: "Payment_Id", newName: "PaymentId");
            AddColumn("dbo.Order", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Order", "BrandId", c => c.Long(nullable: false));
            AlterColumn("dbo.Order", "ClockTypeId", c => c.Long(nullable: false));
            AlterColumn("dbo.Order", "DeliveryId", c => c.Long(nullable: false));
            AlterColumn("dbo.Order", "PaymentId", c => c.Long(nullable: false));
            CreateIndex("dbo.Order", "DeliveryId");
            CreateIndex("dbo.Order", "PaymentId");
            CreateIndex("dbo.Order", "BrandId");
            CreateIndex("dbo.Order", "ClockTypeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "ClockTypeId" });
            DropIndex("dbo.Order", new[] { "BrandId" });
            DropIndex("dbo.Order", new[] { "PaymentId" });
            DropIndex("dbo.Order", new[] { "DeliveryId" });
            AlterColumn("dbo.Order", "PaymentId", c => c.Long());
            AlterColumn("dbo.Order", "DeliveryId", c => c.Long());
            AlterColumn("dbo.Order", "ClockTypeId", c => c.Long());
            AlterColumn("dbo.Order", "BrandId", c => c.Long());
            DropColumn("dbo.Order", "UserId");
            RenameColumn(table: "dbo.Order", name: "PaymentId", newName: "Payment_Id");
            RenameColumn(table: "dbo.Order", name: "DeliveryId", newName: "Delivery_Id");
            RenameColumn(table: "dbo.Order", name: "ClockTypeId", newName: "ClockType_Id");
            RenameColumn(table: "dbo.Order", name: "BrandId", newName: "Brand_Id");
            CreateIndex("dbo.Order", "Payment_Id");
            CreateIndex("dbo.Order", "Delivery_Id");
            CreateIndex("dbo.Order", "ClockType_Id");
            CreateIndex("dbo.Order", "Brand_Id");
        }
    }
}
