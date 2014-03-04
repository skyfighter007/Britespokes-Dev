namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddAdditionalInformation2ToTourMeta : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.TourMeta", "AdditionalInformation2", c => c.String(storeType: "ntext"));
    }

    public override void Down()
    {
      DropColumn("dbo.TourMeta", "AdditionalInformation2");
    }
  }
}