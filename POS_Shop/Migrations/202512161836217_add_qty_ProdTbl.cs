namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_qty_ProdTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Qty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Qty");
        }
    }
}
