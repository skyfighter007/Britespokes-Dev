namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddConfirmationNumberToTraveler : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Travelers", "ConfirmationNumber", c => c.String(nullable: false));
    }

    public override void Down()
    {
      DropColumn("dbo.Travelers", "ConfirmationNumber");
    }
  }
}
