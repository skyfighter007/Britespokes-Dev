using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Departures
{
  public interface IDepartureQueryService
  {
    IEnumerable<Departure> Departures();
    IEnumerable<Departure> Departures(int tourId);
    IEnumerable<Departure> Departures(Tour tour);
    Departure FindDeparture(int productId);
    bool IsCodeUnique(string code, int? productId = null);
  }
}
