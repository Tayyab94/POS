namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurchaseItemIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseItems", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseItems", "IsDeleted");
        }
    }
}
