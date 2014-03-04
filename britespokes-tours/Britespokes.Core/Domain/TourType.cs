using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
  public class TourType : Entity
  {
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Slug { get; set; }

    public virtual List<Tour> Tours { get; set; } 
  }
}