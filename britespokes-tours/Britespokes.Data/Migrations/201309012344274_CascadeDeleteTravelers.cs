namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class CascadeDeleteTravelers : DbMigration
  {
    public override void Up()
    {
      DropForeignKey("dbo.Travelers", "OrderId", "dbo.Orders");
      DropIndex("dbo.Travelers", new[] { "OrderId" });
      AddForeignKey("dbo.Travelers", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
      CreateIndex("dbo.Travelers", "OrderId");
    }

    public override void Down()
    {
      DropIndex("dbo.Travelers", new[] { "OrderId" });
      DropForeignKey("dbo.Travelers", "OrderId", "dbo.Orders");
      CreateIndex("dbo.Travelers", "OrderId");
      AddForeignKey("dbo.Travelers", "OrderId", "dbo.Orders", "Id");
    }
  }
}