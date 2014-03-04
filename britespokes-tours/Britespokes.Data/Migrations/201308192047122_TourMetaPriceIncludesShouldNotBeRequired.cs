namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class TourMetaPriceIncludesShouldNotBeRequired : DbMigration
  {
    public override void Up()
    {
      AlterColumn("dbo.TourMeta", "PriceIncludes1", c => c.String(storeType: "ntext"));
    }

    public override void Down()
    {
      AlterColumn("dbo.TourMeta", "PriceIncludes1", c => c.String(nullable: false, storeType: "ntext"));
    }
  }
}