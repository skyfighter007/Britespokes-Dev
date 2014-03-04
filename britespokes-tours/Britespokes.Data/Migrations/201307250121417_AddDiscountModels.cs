namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddDiscountModels : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Adjustments",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                OrderId = c.Int(nullable: false),
                Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                Description = c.String(),
                IsActive = c.Boolean(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
                DiscountCodeId = c.Int(),
                Discriminator = c.String(nullable: false, maxLength: 128),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
          .ForeignKey("dbo.DiscountCodes", t => t.DiscountCodeId)
          .Index(t => t.OrderId)
          .Index(t => t.DiscountCodeId);

      CreateTable(
          "dbo.DiscountCodes",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Code = c.String(maxLength:50),
                LowerCode = c.String(maxLength:50),
                StartsAt = c.DateTime(),
                ExpiresAt = c.DateTime(),
                IsActive = c.Boolean(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .Index(t => t.LowerCode, unique: true);
    }

    public override void Down()
    {
      DropIndex("dbo.Adjustments", new[] { "DiscountCodeId" });
      DropIndex("dbo.Adjustments", new[] { "OrderId" });
      DropForeignKey("dbo.Adjustments", "DiscountCodeId", "dbo.DiscountCodes");
      DropForeignKey("dbo.Adjustments", "OrderId", "dbo.Orders");
      DropTable("dbo.DiscountCodes");
      DropTable("dbo.Adjustments");
    }
  }
}
