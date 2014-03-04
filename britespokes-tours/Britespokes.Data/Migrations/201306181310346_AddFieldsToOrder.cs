namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddFieldsToOrder : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.Orders", "CompletedAt", c => c.DateTime());
      AddColumn("dbo.Orders", "FailedAt", c => c.DateTime());
      AddColumn("dbo.Orders", "LastFailureMessage", c => c.String());
    }

    public override void Down()
    {
      DropColumn("dbo.Orders", "LastFailureMessage");
      DropColumn("dbo.Orders", "FailedAt");
      DropColumn("dbo.Orders", "CompletedAt");
    }
  }
}