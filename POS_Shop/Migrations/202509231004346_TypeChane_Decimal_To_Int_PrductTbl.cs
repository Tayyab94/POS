namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeChane_Decimal_To_Int_PrductTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PurchasePrice", c => c.Int());
            AlterColumn("dbo.Products", "SalePrice", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "SalePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Products", "PurchasePrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
