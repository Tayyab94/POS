namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrder_OrderDetailtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        OtherProductName = c.String(),
                        Quantity = c.Int(nullable: false),
                        QuantityType = c.String(),
                        Price = c.Single(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalBill = c.Single(nullable: false),
                        ReceiveAmount = c.Single(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        paymentType = c.String(maxLength: 10),
                        customerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.customerId)
                .Index(t => t.customerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
