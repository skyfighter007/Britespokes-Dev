using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Departures
{
  public class DepartureCommandService : IDepartureCommandService
  {
      private readonly IRepository<Departure> _departureRepo;
      private readonly IRepository<Organization> _organizationRepo;

      public DepartureCommandService(IRepository<Departure> departureRepo, IRepository<Organization> organizationRepo)
    {
      _departureRepo = departureRepo;
      _organizationRepo = organizationRepo;
    }

    public Departure Add(DepartureUpdate departureUpdate)
    {
        var organizations = _organizationRepo.All.Where(a => departureUpdate.SelectedOrganizations.Contains(a.Id));
      var departure = new Departure
      {
        TourId = departureUpdate.TourId,
        AvailabilityStatusId = departureUpdate.AvailabilityStatusId,
        Code = departureUpdate.Code,
        DepartureDate = departureUpdate.DepartureDate,
        ReturnDate = departureUpdate.ReturnDate,
        Product = new Product
        {
          Name = departureUpdate.Name,
          Description = departureUpdate.Description,
          AvailableOn = departureUpdate.AvailableOn,
          ProductVariants = new List<ProductVariant>
          {
            new ProductVariant
            {
              Name = "Single",
              DisplayName = "Single",
              PluralDisplayName = "Singles",
              Description = "1 person in the room",
              IsEnabled = true,
              IsMaster = false,
              Price = 0.0m,
              PriceForBriter = 0.0m
            },
            new ProductVariant
            {
              Name = "Double",
              DisplayName = "Double",
              PluralDisplayName = "Doubles",
              Description = "2 people in the room",
              IsEnabled = true,
              IsMaster = true,
               Price = 0.0m,
              PriceForBriter = 0.0m
            },
            new ProductVariant
            {
              Name = "Triple",
              DisplayName = "Triple",
              PluralDisplayName = "Triples",
              Description = "3 people in the room",
              IsEnabled = true,
              IsMaster = false,
              Price = 0.0m,
              PriceForBriter = 0.0m
            },
            new ProductVariant
            {
              Name = "Quad",
              DisplayName = "Quad",
              PluralDisplayName = "Quads",
              Description = "4 people in the room",
              IsEnabled = true,
              IsMaster = false,
              Price = 0.0m,
              PriceForBriter = 0.0m
            },
          }
        }
      };

      departure.Organizations = new System.Collections.Generic.List<Organization>();
      departure.Organizations.AddRange(organizations.ToList());
      _departureRepo.Add(departure);
      return departure;
    }

    public void Update(DepartureUpdate departureUpdate)
    {
        var organizations = _organizationRepo.All.Where(a => departureUpdate.SelectedOrganizations.Contains(a.Id));
      var departure = _departureRepo.FindBy(d => d.ProductId == departureUpdate.ProductId).Single();
      departure.AvailabilityStatusId = departureUpdate.AvailabilityStatusId;
      departure.Code = departureUpdate.Code;
      departure.DepartureDate = departureUpdate.DepartureDate;
      departure.ReturnDate = departureUpdate.ReturnDate;
      departure.Product.Name = departureUpdate.Name;
      departure.Product.Description = departureUpdate.Description;
      departure.Product.AvailableOn = departureUpdate.AvailableOn;

      foreach (var variant in departureUpdate.ProductVariants)
      {
        var productVariant = departure.Product.ProductVariants.Single(v => v.Id == variant.Id);
        productVariant.Price = variant.Price;
        productVariant.PriceForBriter = variant.PriceForBriter;
        productVariant.IsEnabled = variant.IsEnabled;
      }

      List<Organization> exixtingOrganizations = departure.Organizations.ToList();

      foreach (Organization c in exixtingOrganizations)
          departure.Organizations.Remove(c);
      //tour.Categories = null;
      departure.Organizations = new System.Collections.Generic.List<Organization>();
      departure.Organizations.AddRange(organizations.ToList());


      _departureRepo.Update(departure);
    }

    public void Delete(int productId)
    {
      var departure = _departureRepo.FindBy(d => d.ProductId == productId).Single();
      _departureRepo.Delete(departure);
    }
  }
}
