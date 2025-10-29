namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTempOrder_temoOrderDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempOrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        ProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        QuantityType = c.String(),
                        Price = c.Single(nullable: false),
                        TempInvoiceNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TempOrders", t => t.TempInvoiceNumber)
                .Index(t => t.TempInvoiceNumber);
            
            CreateTable(
                "dbo.TempOrders",
                c => new
                    {
                        InvoiceNumber = c.String(nullable: false, maxLength: 128),
                        TotalBill = c.Single(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        customerId = c.Int(),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.InvoiceNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TempOrderDetails", "TempInvoiceNumber", "dbo.TempOrders");
            DropIndex("dbo.TempOrderDetails", new[] { "TempInvoiceNumber" });
            DropTable("dbo.TempOrders");
            DropTable("dbo.TempOrderDetails");
        }
    }
}
