using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Timeline
{
  public interface ITourTimelineCommandService
  {
    void Update(TimelineItemUpdate timelimeItemUpdate);
    void Delete(int itemId);
    void Add(TimelineItemUpdate timelineItemUpdate);
    void UpdatePositions(int tourId, IEnumerable<TourTimelineItem> timelineItems);
  }
}