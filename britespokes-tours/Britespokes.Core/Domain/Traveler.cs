using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class Traveler : Entity
  {
    public Traveler()
    {
      CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }

    public bool EmailItinerary { get; set; }
    public string SpecialInstructions { get; set; }

    public string ConfirmationNumber { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<OrderNote> Notes { get; set; }

    public string FullName
    {
      get { return string.Format("{0} {1}", FirstName, LastName); }
    }
  }
}
