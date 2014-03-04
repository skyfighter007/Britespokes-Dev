using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Departures
{
  public class DepartureQueryService : IDepartureQueryService
  {
    private readonly IRepository<Departure> _departureRepo;

    public DepartureQueryService(IRepository<Departure> departureRepo)
    {
      _departureRepo = departureRepo;
    }

    public IEnumerable<Departure> Departures()
    {
      return _departureRepo.All;
    }

    public IEnumerable<Departure> Departures(int tourId)
    {
      return _departureRepo.FindBy(d => d.TourId == tourId);
    }

    public IEnumerable<Departure> Departures(Tour tour)
    {
      return tour == null ? null : Departures(tour.Id);
    }

    public Departure FindDeparture(int productId)
    {
      return _departureRepo.FindBy(d => d.ProductId == productId).Single();
    }

    public bool IsCodeUnique(string code, int? productId = null)
    {
      var query = _departureRepo.FindBy(t => t.Code == code);
      if (productId.HasValue)
        query = query.Where(t => t.ProductId != productId);
      return query.Any();
    }
  }
}
