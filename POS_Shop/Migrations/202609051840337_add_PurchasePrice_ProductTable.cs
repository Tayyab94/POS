namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_PurchasePrice_ProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PurchasePricePerUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PurchasePricePerUnit");
        }
    }
}
