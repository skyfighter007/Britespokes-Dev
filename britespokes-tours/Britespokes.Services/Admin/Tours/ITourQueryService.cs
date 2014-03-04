using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Tours
{
  public interface ITourQueryService
  {
    IQueryable<Tour> Tours();
    IQueryable<Tour> Tours(string search);
    Tour FindTour(int tourId);
    IEnumerable<AvailabilityStatus> Availabilities();
    IEnumerable<Category> Categories();
    IEnumerable<TourType> TourTypes();
    bool IsCodeUnique(string code, int? tourId = null);
  }
}