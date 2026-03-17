namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerLoan_v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerLedger",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        EntryDate = c.DateTime(nullable: false),
                        EntryType = c.String(nullable: false, maxLength: 30),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReferenceId = c.Int(),
                        ReferenceType = c.String(maxLength: 30),
                        Note = c.String(maxLength: 500),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
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
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMethod = c.String(nullable: false, maxLength: 30),
                        ReferenceNo = c.String(maxLength: 200),
                        Note = c.String(maxLength: 500),
                        LedgerEntryId = c.Int(),
                        CreatedBy = c.String(maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerPayments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerLedger", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerPayments", new[] { "CustomerId" });
            DropIndex("dbo.CustomerLedger", new[] { "CustomerId" });
            DropTable("dbo.CustomerPayments");
            DropTable("dbo.CustomerLedger");
        }
    }
}
