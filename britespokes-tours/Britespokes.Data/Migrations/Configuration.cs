using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Data.Migrations
{
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<EfDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true ;
      AutomaticMigrationDataLossAllowed = false;
    }

    protected override void Seed(EfDbContext context)
    {
      var roles = new List<Role>
        {
          new Role {Name = "Guest"},
          new Role {Name = "Customer"},
          new Role {Name = "Admin"}
        };
      context.Roles.AddOrUpdate(r => r.Name, roles.ToArray());
      context.SaveChanges();

      //var organizations = new List<Organization>
      //  {
      //    new Organization {Name = "Britespokes", IsPublic = true, IsActive = true, IsPasscodeRequired = false}
      //  };
      //context.Organizations.AddOrUpdate(o => o.Name, organizations.ToArray());
      //context.SaveChanges();

      var orderStatuses = new List<OrderStatus>
        {
          new OrderStatus {Name = "Pending"},
          new OrderStatus {Name = "Processing"},
          new OrderStatus {Name = "Complete"},
          new OrderStatus {Name = "Cancelled"},
          new OrderStatus {Name = "Failed"}
        };
      context.OrderStatus.AddOrUpdate(s => s.Name, orderStatuses.ToArray());
      context.SaveChanges();

      var availablityStatuses = new List<AvailabilityStatus>
        {
          new AvailabilityStatus {Id = 1, Name = "Available"},
          new AvailabilityStatus {Id = 2, Name = "Coming Soon"},
          new AvailabilityStatus {Id = 3, Name = "Sold Out"},
          new AvailabilityStatus {Id = 4, Name = "Student Inquiry Form"}
        };
      context.AvailabilityStatus.AddOrUpdate(s => s.Name, availablityStatuses.ToArray());
      context.SaveChanges();

      var tourTypes = new List<TourType>
      {
        new TourType { Name = "Default", DisplayName = "Default", Slug = "" },
        new TourType { Name = "College", DisplayName = "College", Slug = "college" }
      };
      context.TourTypes.AddOrUpdate(t => t.Name, tourTypes.ToArray());
      context.SaveChanges();

      GeoSeeder.Seed(context);
      //TourSeeder.Seed(context, context.Organizations.ToList());
    }
  }
}
