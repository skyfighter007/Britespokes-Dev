using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Tours
{
  public class TourService : ITourService
  {
    private readonly IRepository<Tour> _tourRepository;
    private readonly IRepository<Departure> _departureRepository;

    public TourService(IRepository<Tour> tourRepository, IRepository<Departure> departureRepository)
    {
      _tourRepository = tourRepository;
      _departureRepository = departureRepository;
    }

    public IQueryable<Tour> All()
    {
      return _tourRepository.All;
    }

    public Tour Find(int id)
    {
      return _tourRepository.Find(id);
    }

    public Tour FindByPermalink(string permalink)
    {
      return _tourRepository.FindBy(t => t.Permalink == permalink).SingleOrDefault();
    }

    public Tour FindByCode(string code)
    {
      return _tourRepository.FindBy(t => t.Code == code).SingleOrDefault();
    }

    public Departure FindDeparture(string code)
    {
      return _departureRepository.FindBy(d => d.Code == code).SingleOrDefault();
    }
  }
}
