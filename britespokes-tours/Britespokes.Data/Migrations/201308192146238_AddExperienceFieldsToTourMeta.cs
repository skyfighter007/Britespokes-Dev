namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddExperienceFieldsToTourMeta : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.TourMeta", "ThumbnailImageUrl", c => c.String());
      AddColumn("dbo.TourMeta", "ThumbnailCaption", c => c.String());
      AddColumn("dbo.TourMeta", "JourneyDescription", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.TourMeta", "JourneyDescription");
      DropColumn("dbo.TourMeta", "ThumbnailCaption");
      DropColumn("dbo.TourMeta", "ThumbnailImageUrl");
    }
  }
}