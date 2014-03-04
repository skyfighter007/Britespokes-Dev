namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class Indexes : DbMigration
  {
    public override void Up()
    {
      CreateIndex("dbo.Organizations", "Name", unique: true, name: "UX_Organizations_Name");
      CreateIndex("dbo.Tours", "Permalink", unique: true, name: "UX_Organizations_Permalink");
      CreateIndex("dbo.Tours", "Code", unique: true, name: "UX_Organizations_Code");
      CreateIndex("dbo.Departures", "Code", unique: true, name: "UX_Departures_Code");
      CreateIndex("dbo.OrderStatus", "Name", unique: true, name: "UX_OrderStatus_Name");
      CreateIndex("dbo.Orders", "OrderNumber", unique: true, name: "UX_Orders_OrderNumber");
    }

    public override void Down()
    {
      DropIndex("dbo.Organizations", "UX_Organizations_Name");
      DropIndex("dbo.Tours", "UX_Organizations_Permalink");
      DropIndex("dbo.Tours", "UX_Organizations_Code");
      DropIndex("dbo.Departures", "UX_Departures_Code");
      DropIndex("dbo.OrderStatus", "UX_OrderStatus_Name");
      DropIndex("dbo.Orders", "UX_Orders_OrderNumber");
    }
  }
}
