using System;

namespace Britespokes.Core.Domain
{
  public class OrderNote : Entity
  {
    public OrderNote()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
      DisplayToCustomer = false;
    }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public int TravelerId { get; set; }
    public virtual Traveler Traveler { get; set; }

    public bool DisplayToCustomer { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}
