using System;

namespace Britespokes.Core.Domain
{
  public class Adjustment : Entity
  {
    public Adjustment()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public int OrderId { get; set; }
    public Order Order { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
  }
}