namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applyFullValidationInProductTbl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductEnglishName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "ProductUrduName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "ProductType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductType", c => c.String());
            AlterColumn("dbo.Products", "ProductUrduName", c => c.String());
            AlterColumn("dbo.Products", "ProductEnglishName", c => c.String(nullable: false));
        }
    }
}
