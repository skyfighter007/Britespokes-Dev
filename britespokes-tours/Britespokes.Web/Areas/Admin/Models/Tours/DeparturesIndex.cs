using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
  public class DeparturesIndex
  {
    public int TourId { get; set; }
    public string TourName { get; set; }
    public string TourCode { get; set; }
    public string TourPermalink { get; set; }
    public IEnumerable<Departure> Departures { get; set; }
    public int Count { get; set; }
  }
}