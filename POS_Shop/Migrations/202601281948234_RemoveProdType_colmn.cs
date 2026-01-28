namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProdType_colmn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ProductType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ProductType", c => c.String());
        }
    }
}
