namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  public partial class Initial : DbMigration
  {
    public override void Up()
    {
      CreateTable(
          "dbo.Organizations",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false, maxLength: 255),
                Subdomain = c.String(),
                CustomDomain = c.String(),
                IsPublic = c.Boolean(nullable: false),
                IsActive = c.Boolean(nullable: false),
                IsPasscodeRequired = c.Boolean(nullable: false),
                Passcode = c.String(),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
                RestrictedEmailDomains = c.Boolean(nullable: false),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.Products",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false),
                Description = c.String(),
                AvailableOn = c.DateTime(),
                DeletedAt = c.DateTime(),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.ProductVariants",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(),
                DisplayName = c.String(),
                Description = c.String(),
                Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                IsMaster = c.Boolean(nullable: false),
                IsEnabled = c.Boolean(nullable: false),
                MaxCapacity = c.Int(nullable: false),
                MinCapacity = c.Int(nullable: false),
                MaxChildren = c.Int(nullable: false),
                DeletedAt = c.DateTime(),
                ProductId = c.Int(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
          .Index(t => t.ProductId);

      CreateTable(
          "dbo.Assets",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Width = c.Int(nullable: false),
                Height = c.Int(nullable: false),
                FileSize = c.Int(nullable: false),
                FileName = c.String(),
                ContentType = c.String(),
                AltText = c.String(),
                Data = c.Binary(),
                IsThumbnail = c.Boolean(nullable: false),
                IsFeatured = c.Boolean(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
                ProductVariantId = c.Int(),
                OrganizationId = c.Int(),
                Discriminator = c.String(nullable: false, maxLength: 128),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.ProductVariants", t => t.ProductVariantId, cascadeDelete: true)
          .ForeignKey("dbo.Organizations", t => t.OrganizationId)
          .Index(t => t.ProductVariantId)
          .Index(t => t.OrganizationId);

      CreateTable(
          "dbo.Departures",
          c => new
              {
                ProductId = c.Int(nullable: false),
                TourId = c.Int(nullable: false),
                Code = c.String(nullable: false, maxLength: 50),
                DepartureNumber = c.Int(nullable: false),
                DepartureDate = c.DateTime(nullable: false),
                ReturnDate = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.ProductId)
          .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
          .ForeignKey("dbo.Products", t => t.ProductId)
          .Index(t => t.TourId)
          .Index(t => t.ProductId);

      CreateTable(
          "dbo.Tours",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Code = c.String(nullable: false, maxLength: 50),
                Name = c.String(nullable: false),
                Permalink = c.String(nullable: false, maxLength: 255),
                LengthDescription = c.String(),
                DepartureCity = c.String(),
                ReturningCity = c.String(),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.EmailDomains",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Domain = c.String(nullable: false),
                OrganizationId = c.Int(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
          .Index(t => t.OrganizationId);

      CreateTable(
          "dbo.Users",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                OrganizationId = c.Int(nullable: false),
                Email = c.String(),
                Password = c.String(),
                PasswordSalt = c.String(),
                FirstName = c.String(),
                LastName = c.String(),
                AdminComment = c.String(),
                TimeZoneId = c.String(),
                IsActive = c.Boolean(nullable: false),
                IsDeleted = c.Boolean(nullable: false),
                IsSystemAccount = c.Boolean(nullable: false),
                LastIpAddress = c.String(),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
                LastLoginAt = c.DateTime(),
                LastActivityAt = c.DateTime(nullable: false),
                ConfirmedAt = c.DateTime(),
                ConfirmationToken = c.String(),
                ConfirmationSentAt = c.DateTime(),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
          .Index(t => t.OrganizationId);

      CreateTable(
          "dbo.Roles",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.Countries",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Iso = c.String(maxLength: 2),
                Name = c.String(maxLength: 80),
                PrintableName = c.String(maxLength: 80),
                Iso3 = c.String(maxLength: 3),
                CountryCode = c.Int(),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.ShoppingCartItems",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                UserId = c.Int(nullable: false),
                ProductVariantId = c.Int(nullable: false),
                Quantity = c.Int(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
          .ForeignKey("dbo.ProductVariants", t => t.ProductVariantId, cascadeDelete: true)
          .Index(t => t.UserId)
          .Index(t => t.ProductVariantId);

      CreateTable(
          "dbo.Orders",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                UserId = c.Int(nullable: false),
                BillingAddressId = c.Int(),
                ShippingAddressId = c.Int(),
                OrderStatusId = c.Int(nullable: false),
                OrderNumber = c.String(nullable: false, maxLength: 255),
                SpecialInstructions = c.String(),
                Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                Refund = c.Decimal(nullable: false, precision: 18, scale: 2),
                IsDeleted = c.Boolean(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
          .ForeignKey("dbo.Addresses", t => t.BillingAddressId)
          .ForeignKey("dbo.Addresses", t => t.ShippingAddressId)
          .ForeignKey("dbo.OrderStatus", t => t.OrderStatusId, cascadeDelete: true)
          .Index(t => t.UserId)
          .Index(t => t.BillingAddressId)
          .Index(t => t.ShippingAddressId)
          .Index(t => t.OrderStatusId);

      CreateTable(
          "dbo.Addresses",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                FirstName = c.String(),
                LastName = c.String(),
                Address1 = c.String(),
                Address2 = c.String(),
                City = c.String(),
                ZipCode = c.String(),
                Phone = c.String(),
                MobilePhone = c.String(),
                Company = c.String(),
                StateOrProvince = c.String(),
                CountryId = c.Int(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
          .Index(t => t.CountryId);

      CreateTable(
          "dbo.OrderStatus",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                Name = c.String(nullable: false, maxLength: 255),
              })
          .PrimaryKey(t => t.Id);

      CreateTable(
          "dbo.OrderProductVariants",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                OrderId = c.Int(nullable: false),
                ProductVariantId = c.Int(nullable: false),
                Quantity = c.Int(nullable: false),
                UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.ProductVariants", t => t.ProductVariantId, cascadeDelete: true)
          .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
          .Index(t => t.ProductVariantId)
          .Index(t => t.OrderId);

      CreateTable(
          "dbo.Travelers",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                OrderId = c.Int(nullable: false),
                Email = c.String(),
                FirstName = c.String(),
                LastName = c.String(),
                PhoneNumber = c.String(),
                Birthday = c.DateTime(),
                EmailItinerary = c.Boolean(nullable: false),
                SpecialInstructions = c.String(),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Orders", t => t.OrderId)
          .Index(t => t.OrderId);

      CreateTable(
          "dbo.OrderNotes",
          c => new
              {
                Id = c.Int(nullable: false, identity: true),
                OrderId = c.Int(nullable: false),
                TravelerId = c.Int(),
                DisplayToCustomer = c.Boolean(nullable: false),
                CreatedAt = c.DateTime(nullable: false),
                UpdatedAt = c.DateTime(nullable: false),
              })
          .PrimaryKey(t => t.Id)
          .ForeignKey("dbo.Travelers", t => t.TravelerId)
          .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
          .Index(t => t.TravelerId)
          .Index(t => t.OrderId);

      CreateTable(
          "dbo.OrganizationProducts",
          c => new
              {
                ProductId = c.Int(nullable: false),
                OrganizationId = c.Int(nullable: false),
              })
          .PrimaryKey(t => new { t.ProductId, t.OrganizationId })
          .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
          .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
          .Index(t => t.ProductId)
          .Index(t => t.OrganizationId);

      CreateTable(
          "dbo.UserRoles",
          c => new
              {
                UserId = c.Int(nullable: false),
                RoleId = c.Int(nullable: false),
              })
          .PrimaryKey(t => new { t.UserId, t.RoleId })
          .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
          .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
          .Index(t => t.UserId)
          .Index(t => t.RoleId);

    }

    public override void Down()
    {
      DropIndex("dbo.UserRoles", new[] { "RoleId" });
      DropIndex("dbo.UserRoles", new[] { "UserId" });
      DropIndex("dbo.OrganizationProducts", new[] { "OrganizationId" });
      DropIndex("dbo.OrganizationProducts", new[] { "ProductId" });
      DropIndex("dbo.OrderNotes", new[] { "OrderId" });
      DropIndex("dbo.OrderNotes", new[] { "TravelerId" });
      DropIndex("dbo.Travelers", new[] { "OrderId" });
      DropIndex("dbo.OrderProductVariants", new[] { "OrderId" });
      DropIndex("dbo.OrderProductVariants", new[] { "ProductVariantId" });
      DropIndex("dbo.Addresses", new[] { "CountryId" });
      DropIndex("dbo.Orders", new[] { "OrderStatusId" });
      DropIndex("dbo.Orders", new[] { "ShippingAddressId" });
      DropIndex("dbo.Orders", new[] { "BillingAddressId" });
      DropIndex("dbo.Orders", new[] { "UserId" });
      DropIndex("dbo.ShoppingCartItems", new[] { "ProductVariantId" });
      DropIndex("dbo.ShoppingCartItems", new[] { "UserId" });
      DropIndex("dbo.Users", new[] { "OrganizationId" });
      DropIndex("dbo.EmailDomains", new[] { "OrganizationId" });
      DropIndex("dbo.Departures", new[] { "ProductId" });
      DropIndex("dbo.Departures", new[] { "TourId" });
      DropIndex("dbo.Assets", new[] { "OrganizationId" });
      DropIndex("dbo.Assets", new[] { "ProductVariantId" });
      DropIndex("dbo.ProductVariants", new[] { "ProductId" });
      DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
      DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
      DropForeignKey("dbo.OrganizationProducts", "OrganizationId", "dbo.Organizations");
      DropForeignKey("dbo.OrganizationProducts", "ProductId", "dbo.Products");
      DropForeignKey("dbo.OrderNotes", "OrderId", "dbo.Orders");
      DropForeignKey("dbo.OrderNotes", "TravelerId", "dbo.Travelers");
      DropForeignKey("dbo.Travelers", "OrderId", "dbo.Orders");
      DropForeignKey("dbo.OrderProductVariants", "OrderId", "dbo.Orders");
      DropForeignKey("dbo.OrderProductVariants", "ProductVariantId", "dbo.ProductVariants");
      DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
      DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
      DropForeignKey("dbo.Orders", "ShippingAddressId", "dbo.Addresses");
      DropForeignKey("dbo.Orders", "BillingAddressId", "dbo.Addresses");
      DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
      DropForeignKey("dbo.ShoppingCartItems", "ProductVariantId", "dbo.ProductVariants");
      DropForeignKey("dbo.ShoppingCartItems", "UserId", "dbo.Users");
      DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
      DropForeignKey("dbo.EmailDomains", "OrganizationId", "dbo.Organizations");
      DropForeignKey("dbo.Departures", "ProductId", "dbo.Products");
      DropForeignKey("dbo.Departures", "TourId", "dbo.Tours");
      DropForeignKey("dbo.Assets", "OrganizationId", "dbo.Organizations");
      DropForeignKey("dbo.Assets", "ProductVariantId", "dbo.ProductVariants");
      DropForeignKey("dbo.ProductVariants", "ProductId", "dbo.Products");
      DropTable("dbo.UserRoles");
      DropTable("dbo.OrganizationProducts");
      DropTable("dbo.OrderNotes");
      DropTable("dbo.Travelers");
      DropTable("dbo.OrderProductVariants");
      DropTable("dbo.OrderStatus");
      DropTable("dbo.Addresses");
      DropTable("dbo.Orders");
      DropTable("dbo.ShoppingCartItems");
      DropTable("dbo.Countries");
      DropTable("dbo.Roles");
      DropTable("dbo.Users");
      DropTable("dbo.EmailDomains");
      DropTable("dbo.Tours");
      DropTable("dbo.Departures");
      DropTable("dbo.Assets");
      DropTable("dbo.ProductVariants");
      DropTable("dbo.Products");
      DropTable("dbo.Organizations");
    }
  }
}
