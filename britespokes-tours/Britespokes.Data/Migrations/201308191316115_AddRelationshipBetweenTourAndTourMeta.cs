namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddRelationshipBetweenTourAndTourMeta : DbMigration
  {
    public override void Up()
    {
      DropForeignKey("dbo.TourMeta", "TourId", "dbo.Tours");
      DropIndex("dbo.TourMeta", new[] { "TourId" });
      AddForeignKey("dbo.TourMeta", "TourId", "dbo.Tours", "Id", cascadeDelete: true);
      CreateIndex("dbo.TourMeta", "TourId");
    }

    public override void Down()
    {
      DropIndex("dbo.TourMeta", new[] { "TourId" });
      DropForeignKey("dbo.TourMeta", "TourId", "dbo.Tours");
      CreateIndex("dbo.TourMeta", "TourId");
      AddForeignKey("dbo.TourMeta", "TourId", "dbo.Tours", "Id");
    }
  }
}