namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddTourMetaAndIsPublished : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.TourMeta",
          c => new
              {
                TourId = c.Int(nullable: false),
                Description = c.String(nullable: false, storeType: "ntext"),
                PriceIncludes1 = c.String(nullable: false, storeType: "ntext"),
                PriceIncludes2 = c.String(storeType: "ntext"),
                BannerImageUrl = c.String(),
              })
          .PrimaryKey(t => t.TourId)
          .ForeignKey("dbo.Tours", t => t.TourId)
          .Index(t => t.TourId);

      AddColumn("dbo.Tours", "IsPublished", c => c.Boolean(nullable: false, defaultValue: false));
    }

    public override void Down()
    {
      DropIndex("dbo.TourMeta", new[] { "TourId" });
      DropForeignKey("dbo.TourMeta", "TourId", "dbo.Tours");
      DropColumn("dbo.Tours", "IsPublished");
      DropTable("dbo.TourMeta");
    }
  }
}