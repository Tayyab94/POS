namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_Prod_UniPrice_TotalActualBill_Profit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductPrices", "PurchasePricePerUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "TotalActualBill", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "TotalProfit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalProfit");
            DropColumn("dbo.Orders", "TotalActualBill");
            DropColumn("dbo.ProductPrices", "PurchasePricePerUnit");
        }
    }
}
