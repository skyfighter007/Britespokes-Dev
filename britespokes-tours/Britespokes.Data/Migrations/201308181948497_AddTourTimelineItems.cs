namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddTourTimelineItems : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.TourTimelineItems",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                TourId = c.Int(nullable: false),
                ImageUrl = c.String(nullable: false),
                Caption = c.String(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
          .Index(t => t.TourId);

    }

    public override void Down()
    {
      DropIndex("dbo.TourTimelineItems", new[] { "TourId" });
      DropForeignKey("dbo.TourTimelineItems", "TourId", "dbo.Tours");
      DropTable("dbo.TourTimelineItems");
    }
  }
}