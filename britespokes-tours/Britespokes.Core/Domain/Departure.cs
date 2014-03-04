using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class Departure : BaseEntity
  {
    public int TourId { get; set; }
    public virtual Tour Tour { get; set; }
    public string Code { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    public int? AvailabilityStatusId { get; set; }
    public virtual AvailabilityStatus AvailabilityStatus { get; set; }

    public virtual List<Organization> Organizations { get; set; }
      
  }
}