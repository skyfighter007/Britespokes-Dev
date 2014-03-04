namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddPriceIncludesAndAdditionalInformationHeadersToTourMeta : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.TourMeta", "PriceIncludesHeader", c => c.String());
      AddColumn("dbo.TourMeta", "AdditionalInformationHeader", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.TourMeta", "AdditionalInformationHeader");
      DropColumn("dbo.TourMeta", "PriceIncludesHeader");
    }
  }
}