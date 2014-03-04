namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class AddManyToManyDiscountCodeRelationships : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.TourDiscountCodes",
          c => new
              {
                DiscountCodeId = c.Int(nullable: false),
                TourId = c.Int(nullable: false),
              })
          .PrimaryKey(t => new { t.DiscountCodeId, t.TourId })
          .ForeignKey("dbo.DiscountCodes", t => t.DiscountCodeId, cascadeDelete: true)
          .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
          .Index(t => t.DiscountCodeId)
          .Index(t => t.TourId);

      CreateTable(
          "dbo.ProductDiscountCodes",
          c => new
              {
                DiscountCodeId = c.Int(nullable: false),
                ProductId = c.Int(nullable: false),
              })
          .PrimaryKey(t => new { t.DiscountCodeId, t.ProductId })
          .ForeignKey("dbo.DiscountCodes", t => t.DiscountCodeId, cascadeDelete: true)
          .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
          .Index(t => t.DiscountCodeId)
          .Index(t => t.ProductId);

      CreateTable(
          "dbo.ProductVariantDiscountCodes",
          c => new
              {
                DiscountCodeId = c.Int(nullable: false),
                ProductVariantId = c.Int(nullable: false),
              })
          .PrimaryKey(t => new { t.DiscountCodeId, t.ProductVariantId })
          .ForeignKey("dbo.DiscountCodes", t => t.DiscountCodeId, cascadeDelete: true)
          .ForeignKey("dbo.ProductVariants", t => t.ProductVariantId, cascadeDelete: true)
          .Index(t => t.DiscountCodeId)
          .Index(t => t.ProductVariantId);

      AlterColumn("dbo.DiscountCodes", "Code", c => c.String(nullable: false));
    }

    public override void Down()
    {
      DropIndex("dbo.ProductVariantDiscountCodes", new[] { "ProductVariantId" });
      DropIndex("dbo.ProductVariantDiscountCodes", new[] { "DiscountCodeId" });
      DropIndex("dbo.ProductDiscountCodes", new[] { "ProductId" });
      DropIndex("dbo.ProductDiscountCodes", new[] { "DiscountCodeId" });
      DropIndex("dbo.TourDiscountCodes", new[] { "TourId" });
      DropIndex("dbo.TourDiscountCodes", new[] { "DiscountCodeId" });
      DropForeignKey("dbo.ProductVariantDiscountCodes", "ProductVariantId", "dbo.ProductVariants");
      DropForeignKey("dbo.ProductVariantDiscountCodes", "DiscountCodeId", "dbo.DiscountCodes");
      DropForeignKey("dbo.ProductDiscountCodes", "ProductId", "dbo.Products");
      DropForeignKey("dbo.ProductDiscountCodes", "DiscountCodeId", "dbo.DiscountCodes");
      DropForeignKey("dbo.TourDiscountCodes", "TourId", "dbo.Tours");
      DropForeignKey("dbo.TourDiscountCodes", "DiscountCodeId", "dbo.DiscountCodes");
      AlterColumn("dbo.DiscountCodes", "Code", c => c.String());
      DropTable("dbo.ProductVariantDiscountCodes");
      DropTable("dbo.ProductDiscountCodes");
      DropTable("dbo.TourDiscountCodes");
    }
  }
}