namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SearchByProdCodeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "SearchByProductCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "SearchByProductCode");
        }
    }
}
