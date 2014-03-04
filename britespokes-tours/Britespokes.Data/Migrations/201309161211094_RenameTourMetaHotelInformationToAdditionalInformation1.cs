namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class RenameTourMetaHotelInformationToAdditionalInformation1 : DbMigration
  {
    public override void Up()
    {
      RenameColumn("dbo.TourMeta", "HotelInformation", "AdditionalInformation1");
    }

    public override void Down()
    {
      RenameColumn("dbo.TourMeta", "AdditionalInformation1", "HotelInformation");
    }
  }
}