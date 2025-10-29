namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Require_contactNo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "ContactNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ContactNo", c => c.String(nullable: false));
        }
    }
}
