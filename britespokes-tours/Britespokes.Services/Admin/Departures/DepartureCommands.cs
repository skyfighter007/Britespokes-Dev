using System;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.Departures
{
  public class DepartureUpdate
  {
    public int TourId { get; set; }
    public int ProductId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int AvailabilityStatusId { get; set; }
    public DateTime? AvailableOn { get; set; }

    public List<ProductVariantUpdate> ProductVariants { get; set; }
    public List<int> SelectedOrganizations { get; set; }
  }

  public class ProductVariantUpdate
  {
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal PriceForBriter { get; set; }
    public bool IsEnabled { get; set; }
  }
}
