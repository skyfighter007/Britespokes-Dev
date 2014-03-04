using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class Order : Entity
  {
    public Order()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
      IsDeleted = false;
    }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public int? BillingAddressId { get; set; }
    public virtual Address BillingAddress { get; set; }
    // for future use
    public int? ShippingAddressId { get; set; }
    public virtual Address ShippingAddress { get; set; }

    public int OrderStatusId { get; set; }
    public virtual OrderStatus OrderStatus { get; set; }

    public string OrderNumber { get; set; }
    public string SpecialInstructions { get; set; }

    public decimal Total { get; set; }
    public decimal ItemTotal { get; set; }
    public decimal AdjustmentTotal { get; set; }
    public string ChargeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual List<OrderProductVariant> ProductVariants { get; set; }
    public virtual List<Traveler> Travelers { get; set; }
    public virtual List<OrderNote> Notes { get; set; }
    public virtual List<Adjustment> Adjustments { get; set; }

    public DateTime? CompletedAt { get; set; }
    public DateTime? FailedAt { get; set; }
    public string LastFailureMessage { get; set; }
  }
}