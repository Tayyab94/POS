namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSalePrice_ProdTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "SalePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "SalePrice", c => c.Int());
        }
    }
}
