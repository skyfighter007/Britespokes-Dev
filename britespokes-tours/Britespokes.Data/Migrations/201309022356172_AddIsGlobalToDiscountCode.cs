namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddIsGlobalToDiscountCode : DbMigration
  {
    public override void Up()
    {
      AddColumn("dbo.DiscountCodes", "IsGlobal", c => c.Boolean(nullable: false, defaultValue: true));
    }

    public override void Down()
    {
      DropColumn("dbo.DiscountCodes", "IsGlobal");
    }
  }
}