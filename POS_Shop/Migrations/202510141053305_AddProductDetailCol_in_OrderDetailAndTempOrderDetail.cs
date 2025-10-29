namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductDetailCol_in_OrderDetailAndTempOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductDetail", c => c.String());
            AddColumn("dbo.TempOrderDetails", "ProductDetail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TempOrderDetails", "ProductDetail");
            DropColumn("dbo.OrderDetails", "ProductDetail");
        }
    }
}
