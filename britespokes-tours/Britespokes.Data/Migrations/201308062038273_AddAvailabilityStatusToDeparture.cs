namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddAvailabilityStatusToDeparture : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Departures", "AvailabilityStatusId", c => c.Int());
      AddForeignKey("dbo.Departures", "AvailabilityStatusId", "dbo.AvailabilityStatus", "Id");
      CreateIndex("dbo.Departures", "AvailabilityStatusId");
    }

    public override void Down()
    {
      DropIndex("dbo.Departures", new[] { "AvailabilityStatusId" });
      DropForeignKey("dbo.Departures", "AvailabilityStatusId", "dbo.AvailabilityStatus");
      DropColumn("dbo.Departures", "AvailabilityStatusId");
    }
  }
}