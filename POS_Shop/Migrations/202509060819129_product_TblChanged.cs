namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_TblChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductEnglishName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductEnglishName", c => c.String());
        }
    }
}
