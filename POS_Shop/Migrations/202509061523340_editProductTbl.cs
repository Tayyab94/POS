namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editProductTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductType", c => c.String());
            AlterColumn("dbo.Products", "PurchasePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "SalePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Cost", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Cost", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "ProductType", c => c.String(nullable: false));
        }
    }
}
