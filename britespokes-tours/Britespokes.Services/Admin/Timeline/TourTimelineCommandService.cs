using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Timeline
{
  public class TourTimelineCommandService : ITourTimelineCommandService
  {
    private readonly IRepository<TourTimelineItem> _tourTimelineItemRepo;

    public TourTimelineCommandService(IRepository<TourTimelineItem> tourTimelineItemRepo)
    {
      _tourTimelineItemRepo = tourTimelineItemRepo;
    }

    public void Update(TimelineItemUpdate timelimeItemUpdate)
    {
      var timelineItem = FindItem(timelimeItemUpdate.Id);
      timelineItem.ImageUrl = timelimeItemUpdate.ImageUrl;
      timelineItem.Caption = timelimeItemUpdate.Caption;

      _tourTimelineItemRepo.Update(timelineItem);
    }

    public void Delete(int itemId)
    {
      _tourTimelineItemRepo.Delete(itemId);
    }

    public void Add(TimelineItemUpdate timelineItemUpdate)
    {
      var position = 0;
      var items = _tourTimelineItemRepo.FindBy(i => i.TourId == timelineItemUpdate.TourId);
      if (items.Any())
        position = items.Max(i => i.Position) + 1;

      _tourTimelineItemRepo.Add(new TourTimelineItem
      {
        TourId = timelineItemUpdate.TourId,
        ImageUrl = timelineItemUpdate.ImageUrl,
        Caption = timelineItemUpdate.Caption,
        Position = position
      });
    }

    public void UpdatePositions(int tourId, IEnumerable<TourTimelineItem> timelineItems)
    {
      var items = _tourTimelineItemRepo.FindBy(i => i.TourId == tourId);

      foreach (var timelineItem in timelineItems)
      {
        var actualTimelineItem = items.Single(i => i.Id == timelineItem.Id);
        actualTimelineItem.Position = timelineItem.Position;
        _tourTimelineItemRepo.Update(actualTimelineItem);
      }
    }

    private TourTimelineItem FindItem(int id)
    {
      return _tourTimelineItemRepo.Find(id);
    }
  }
}