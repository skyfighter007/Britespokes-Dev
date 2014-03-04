namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddAvailabilityStatus : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.AvailabilityStatus",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false, maxLength: 255),
              })
          .PrimaryKey(t => t.Id);

    }

    public override void Down()
    {
      DropTable("dbo.AvailabilityStatus");
    }
  }
}