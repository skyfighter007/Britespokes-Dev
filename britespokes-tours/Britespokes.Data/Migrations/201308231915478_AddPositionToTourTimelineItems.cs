namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddPositionToTourTimelineItems : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.TourTimelineItems", "Position", c => c.Int(nullable: false));
    }

    public override void Down()
    {
      DropColumn("dbo.TourTimelineItems", "Position");
    }
  }
}