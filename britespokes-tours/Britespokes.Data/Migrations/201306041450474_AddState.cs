namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddState : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.States",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(),
                Abbreviation = c.String(),
              })
          .PrimaryKey(t => t.Id);
    }

    public override void Down()
    {
      DropTable("dbo.States");
    }
  }
}