using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Timeline
{
  public class TourTimelineQueryService : ITourTimelineQueryService
  {
    private readonly IRepository<TourTimelineItem> _tourTimelineItemRepo;

    public TourTimelineQueryService(IRepository<TourTimelineItem> tourTimelineItemRepo)
    {
      _tourTimelineItemRepo = tourTimelineItemRepo;
    }

    public IQueryable<TourTimelineItem> TimelineItems(int tourId)
    {
      return _tourTimelineItemRepo.FindBy(i => i.TourId == tourId);
    }

    public IQueryable<TourTimelineItem> TimelineItems(Tour tour)
    {
      return TimelineItems(tour.Id);
    }

    public TourTimelineItem FindTimelineItem(int itemId)
    {
      return _tourTimelineItemRepo.Find(itemId);
    }
  }
}