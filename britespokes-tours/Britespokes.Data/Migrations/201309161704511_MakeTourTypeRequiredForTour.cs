namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class MakeTourTypeRequiredForTour : DbMigration
  {
    public override void Up()
    {
      DropForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes");
      DropIndex("dbo.Tours", new[] { "TourTypeId" });
      AlterColumn("dbo.Tours", "TourTypeId", c => c.Int(nullable: false));
      AddForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes", "Id", cascadeDelete: true);
      CreateIndex("dbo.Tours", "TourTypeId");
    }

    public override void Down()
    {
      DropIndex("dbo.Tours", new[] { "TourTypeId" });
      DropForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes");
      AlterColumn("dbo.Tours", "TourTypeId", c => c.Int());
      CreateIndex("dbo.Tours", "TourTypeId");
      AddForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes", "Id");
    }
  }
}