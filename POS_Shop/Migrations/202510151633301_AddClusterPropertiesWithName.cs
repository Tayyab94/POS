namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClusterPropertiesWithName : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Products", "ProductEnglishName", name: "IX_Product_EnglishName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", "IX_Product_EnglishName");
        }
    }
}
