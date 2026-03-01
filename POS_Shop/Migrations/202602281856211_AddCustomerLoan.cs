namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerLoan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RunningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LastTransactionDate = c.DateTime(nullable: false),
                        Notes = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerPayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.String(nullable: false, maxLength: 30),
                        ReferenceNo = c.String(maxLength: 100),
                        TransactionId = c.String(maxLength: 100),
                        Notes = c.String(maxLength: 500),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        BalanceBefore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalanceAfter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(maxLength: 100),
                        DeletedAt = c.DateTime(),
                        DeleteReason = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomerTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.String(nullable: false, maxLength: 20),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DebitCredit = c.String(nullable: false, maxLength: 1),
                        BalanceAfter = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(),
                        CustomerPaymentId = c.Int(),
                        ReferenceNo = c.String(maxLength: 100),
                        Notes = c.String(maxLength: 500),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.CustomerPayments", t => t.CustomerPaymentId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.CustomerId)
                .Index(t => t.OrderId)
                .Index(t => t.CustomerPaymentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerTransactions", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.CustomerTransactions", "CustomerPaymentId", "dbo.CustomerPayments");
            DropForeignKey("dbo.CustomerTransactions", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerPayments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerLedgers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerTransactions", new[] { "CustomerPaymentId" });
            DropIndex("dbo.CustomerTransactions", new[] { "OrderId" });
            DropIndex("dbo.CustomerTransactions", new[] { "CustomerId" });
            DropIndex("dbo.CustomerPayments", new[] { "CustomerId" });
            DropIndex("dbo.CustomerLedgers", new[] { "CustomerId" });
            DropTable("dbo.CustomerTransactions");
            DropTable("dbo.CustomerPayments");
            DropTable("dbo.CustomerLedgers");
        }
    }
}
