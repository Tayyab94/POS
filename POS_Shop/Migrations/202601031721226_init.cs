namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        categoryId = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Categories", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductEnglishName = c.String(nullable: false, maxLength: 50),
                        ProductUrduName = c.String(nullable: false, maxLength: 50),
                        ProductType = c.String(),
                        PurchasePrice = c.String(),
                        SalePrice = c.Int(),
                        Cost = c.Int(),
                        Qty = c.Int(nullable: false),
                        SearchByProductCode = c.String(),
                        SubcategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubcategoryId)
                .Index(t => t.ProductEnglishName, name: "IX_Product_EnglishName")
                .Index(t => t.SubcategoryId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CountryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        CustomerAddress = c.String(nullable: false),
                        ContactNo = c.String(),
                        CityId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
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
                        CreatedDate = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductDetail = c.String(),
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
                        ProductDetail = c.String(),
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
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Products", "SubcategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "categoryId", "dbo.Categories");
            DropIndex("dbo.TempOrderDetails", new[] { "TempInvoiceNumber" });
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.Customers", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Products", new[] { "SubcategoryId" });
            DropIndex("dbo.Products", "IX_Product_EnglishName");
            DropIndex("dbo.SubCategories", new[] { "categoryId" });
            DropTable("dbo.TempOrders");
            DropTable("dbo.TempOrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Products");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}
