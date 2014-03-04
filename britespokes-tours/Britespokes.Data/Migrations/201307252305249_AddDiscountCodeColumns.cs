namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddDiscountCodeColumns : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.DiscountCodes", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
      AddColumn("dbo.DiscountCodes", "Percentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
      AddColumn("dbo.DiscountCodes", "UsePercentage", c => c.Boolean(nullable: false));
    }

    public override void Down()
    {
      DropColumn("dbo.DiscountCodes", "UsePercentage");
      DropColumn("dbo.DiscountCodes", "Percentage");
      DropColumn("dbo.DiscountCodes", "Amount");
    }
  }
}