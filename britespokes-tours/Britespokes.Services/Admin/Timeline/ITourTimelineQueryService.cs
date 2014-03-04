using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Timeline
{
  public interface ITourTimelineQueryService
  {
    IQueryable<TourTimelineItem> TimelineItems(int tourId);
    IQueryable<TourTimelineItem> TimelineItems(Tour tour);
    TourTimelineItem FindTimelineItem(int itemId);
  }
}