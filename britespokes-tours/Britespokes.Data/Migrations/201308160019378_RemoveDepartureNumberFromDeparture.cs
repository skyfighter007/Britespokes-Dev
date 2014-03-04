namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class RemoveDepartureNumberFromDeparture : DbMigration
  {
    public override void Up()
    {
      DropColumn("dbo.Departures", "DepartureNumber");
    }

    public override void Down()
    {
      AddColumn("dbo.Departures", "DepartureNumber", c => c.Int(nullable: false));
    }
  }
}