namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ProdStockUnit_Prod_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProdQtyStockUnit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProdQtyStockUnit");
        }
    }
}
