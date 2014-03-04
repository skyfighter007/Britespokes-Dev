namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddHotelInformationToTourMeta : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.TourMeta", "HotelInformation", c => c.String(storeType: "ntext"));
    }

    public override void Down()
    {
      DropColumn("dbo.TourMeta", "HotelInformation");
    }
  }
}