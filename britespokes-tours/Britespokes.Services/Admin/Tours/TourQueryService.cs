using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Tours
{
  public class TourQueryService : ITourQueryService
  {
    private readonly IRepository<Tour> _tourRepo;
    private readonly IRepository<AvailabilityStatus> _availabilityRepo;
    private readonly IRepository<Category> _CategoryRepo;
    private readonly IRepository<TourType> _tourTypeRepo;

    public TourQueryService(IRepository<Tour> tourRepo, IRepository<AvailabilityStatus> availabilityRepo, IRepository<TourType> tourTypeRepo,IRepository<Category> CategoryRepo)
    {
      _tourRepo = tourRepo;
      _availabilityRepo = availabilityRepo;
      _CategoryRepo = CategoryRepo;
      _tourTypeRepo = tourTypeRepo;
    }

    public IQueryable<Tour> Tours()
    {
      return _tourRepo.All;
    }

    public IQueryable<Tour> Tours(string search)
    {
      if (string.IsNullOrWhiteSpace(search))
        return Tours();

      var q = search.ToLowerInvariant();
      return _tourRepo.FindBy(t => t.Code.ToLower().Contains(q) || t.Name.ToLower().Contains(q));
    }

    public Tour FindTour(int tourId)
    {
      return _tourRepo.Find(tourId);
    }

    public IEnumerable<AvailabilityStatus> Availabilities()
    {
      return _availabilityRepo.All;
    }

    public IEnumerable<Category> Categories()
    {
        return _CategoryRepo.All;
    }

    public IEnumerable<TourType> TourTypes()
    {
      return _tourTypeRepo.All;
    }

    public bool IsCodeUnique(string code, int? tourId = null)
    {
      var query = _tourRepo.FindBy(t => t.Code == code);
      if (tourId.HasValue)
        query = query.Where(t => t.Id != tourId);
      return query.Any();
    }
  }
}