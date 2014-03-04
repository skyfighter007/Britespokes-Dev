namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddExperiences : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Experiences",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                TourId = c.Int(nullable: false),
                Position = c.Int(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
          .Index(t => t.TourId);

    }

    public override void Down()
    {
      DropIndex("dbo.Experiences", new[] { "TourId" });
      DropForeignKey("dbo.Experiences", "TourId", "dbo.Tours");
      DropTable("dbo.Experiences");
    }
  }
}