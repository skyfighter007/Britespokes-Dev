using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Orders
{
  public class OrderSummary
  {
    public int Id { get; set; }
    public Order Order { get; set; }
    public Dictionary<string, int> OrderVariants { get; set; }

    public Tour[] Tours;
    public Departure[] Departures;
    public DiscountCode[] DiscountCodes;
  }
}
