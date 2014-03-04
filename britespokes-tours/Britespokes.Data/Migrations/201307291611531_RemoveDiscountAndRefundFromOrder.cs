namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class RemoveDiscountAndRefundFromOrder : DbMigration
  {
    public override void Up()
    {
      DropColumn("dbo.Orders", "Discount");
      DropColumn("dbo.Orders", "Refund");
    }

    public override void Down()
    {
      AddColumn("dbo.Orders", "Refund", c => c.Decimal(nullable: false, precision: 18, scale: 2));
      AddColumn("dbo.Orders", "Discount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
    }
  }
}
