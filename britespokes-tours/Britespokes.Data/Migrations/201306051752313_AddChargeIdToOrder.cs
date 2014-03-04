namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddChargeIdToOrder : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Orders", "ChargeId", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.Orders", "ChargeId");
    }
  }
}