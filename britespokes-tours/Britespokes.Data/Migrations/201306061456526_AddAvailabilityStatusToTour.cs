namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddAvailabilityStatusToTour : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Tours", "AvailabilityStatusId", c => c.Int());
      AddForeignKey("dbo.Tours", "AvailabilityStatusId", "dbo.AvailabilityStatus", "Id");
      CreateIndex("dbo.Tours", "AvailabilityStatusId");
    }

    public override void Down()
    {
      DropIndex("dbo.Tours", new[] { "AvailabilityStatusId" });
      DropForeignKey("dbo.Tours", "AvailabilityStatusId", "dbo.AvailabilityStatus");
      DropColumn("dbo.Tours", "AvailabilityStatusId");
    }
  }
}