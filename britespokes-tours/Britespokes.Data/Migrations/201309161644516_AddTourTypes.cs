namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddTourTypes : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.TourTypes",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false, maxLength: 255),
                DisplayName = c.String(nullable: false, maxLength: 255),
                Slug = c.String(),
              })
          .PrimaryKey(t => t.Id);

      AddColumn("dbo.Tours", "TourTypeId", c => c.Int());
      AddForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes", "Id");
      CreateIndex("dbo.Tours", "TourTypeId");
    }

    public override void Down()
    {
      DropIndex("dbo.Tours", new[] { "TourTypeId" });
      DropForeignKey("dbo.Tours", "TourTypeId", "dbo.TourTypes");
      DropColumn("dbo.Tours", "TourTypeId");
      DropTable("dbo.TourTypes");
    }
  }
}