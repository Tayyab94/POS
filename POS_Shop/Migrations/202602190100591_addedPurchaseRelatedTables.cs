namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPurchaseRelatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductUnitId = c.Int(),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductUnits", t => t.ProductUnitId)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductUnitId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(nullable: false, maxLength: 30),
                        SupplierReferenceNo = c.String(maxLength: 50),
                        PurchaseDate = c.DateTime(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentStatus = c.Int(nullable: false),
                        Notes = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.SupplierPaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierPaymentId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                        AmountAllocated = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId)
                .ForeignKey("dbo.SupplierPayments", t => t.SupplierPaymentId)
                .Index(t => t.SupplierPaymentId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "dbo.SupplierPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentNumber = c.String(nullable: false, maxLength: 30),
                        SupplierId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        TotalAmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAllocated = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.Int(nullable: false),
                        TransactionReference = c.String(maxLength: 100),
                        Notes = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseItems", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.Purchases", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierPaymentDetails", "SupplierPaymentId", "dbo.SupplierPayments");
            DropForeignKey("dbo.SupplierPayments", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.SupplierPaymentDetails", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchaseItems", "ProductUnitId", "dbo.ProductUnits");
            DropForeignKey("dbo.PurchaseItems", "ProductId", "dbo.Products");
            DropIndex("dbo.SupplierPayments", new[] { "SupplierId" });
            DropIndex("dbo.SupplierPaymentDetails", new[] { "PurchaseId" });
            DropIndex("dbo.SupplierPaymentDetails", new[] { "SupplierPaymentId" });
            DropIndex("dbo.Purchases", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseItems", new[] { "ProductUnitId" });
            DropIndex("dbo.PurchaseItems", new[] { "ProductId" });
            DropIndex("dbo.PurchaseItems", new[] { "PurchaseId" });
            DropTable("dbo.SupplierPayments");
            DropTable("dbo.SupplierPaymentDetails");
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchaseItems");
        }
    }
}
