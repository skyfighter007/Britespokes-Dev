namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class BillingPageSupport : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.ProductVariants", "PluralDisplayName", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.ProductVariants", "PluralDisplayName");
    }
  }
}
