namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_SalePriceCol_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "PurchasePrice", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "PurchasePrice", c => c.Int());
        }
    }
}
