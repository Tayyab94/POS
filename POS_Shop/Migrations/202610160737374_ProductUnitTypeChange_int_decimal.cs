namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUnitTypeChange_int_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Qty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.TempOrderDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TempOrderDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Price", c => c.Single(nullable: false));
            AlterColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "Qty", c => c.Int(nullable: false));
        }
    }
}
