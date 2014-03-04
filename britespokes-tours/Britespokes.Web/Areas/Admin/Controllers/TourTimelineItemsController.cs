using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Timeline;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Admin.Models.Timeline;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class TourTimelineItemsController : AdminBaseController
  {
    private readonly ITourTimelineQueryService _tourTimelineQueryService;
    private readonly ITourTimelineCommandService _tourTimelineCommandService;
    private readonly ITourQueryService _tourQueryService;

    public TourTimelineItemsController(ITourTimelineQueryService tourTimelineQueryService,
      ITourTimelineCommandService tourTimelineCommandService,
      ITourQueryService tourQueryService)
    {
      _tourTimelineQueryService = tourTimelineQueryService;
      _tourTimelineCommandService = tourTimelineCommandService;
      _tourQueryService = tourQueryService;
    }

    public ActionResult Index(int tourId)
    {
      var tour = _tourQueryService.FindTour(tourId);
      if (tour == null)
        return HttpNotFound();

      var timelineItems = _tourTimelineQueryService.TimelineItems(tourId).OrderBy(i => i.Position);
      return View(new TimelineIndex
      {
        TourId = tour.Id,
        TourName = tour.Name,
        TourCode = tour.Code,
        TourPermalink = tour.Permalink,
        Count = timelineItems.Count(),
        Items = timelineItems
      });
    }

    public ActionResult Edit(int itemId)
    {
      var item = _tourTimelineQueryService.FindTimelineItem(itemId);
      var timelineItemEdit = Mapper.Map<TimelineItemEdit>(item);
      return View(timelineItemEdit);
    }

    [HttpPost]
    public ActionResult Edit(TimelineItemEdit timelineItemEdit)
    {
      if (!ModelState.IsValid)
      {
        return View(timelineItemEdit);
      }

      var timelimeItemUpdate = Mapper.Map<TimelineItemUpdate>(timelineItemEdit);
      _tourTimelineCommandService.Update(timelimeItemUpdate);
      TempData [ "Info" ] = "Timeline item updated";
      return RedirectToRoute("admin-tour-timeline-items");
    }

    public ActionResult Create(int tourId)
    {
      var tour = _tourQueryService.FindTour(tourId);
      return View(new TimelineItemEdit
      {
        TourId = tour.Id,
        TourName = tour.Name,
        TourCode = tour.Code,
        TourPermalink = tour.Permalink
      });
    }

    [HttpPost]
    public ActionResult Create(TimelineItemEdit timelineItemEdit)
    {
      if (!ModelState.IsValid) return View(timelineItemEdit);

      var timelineItemUpdate = Mapper.Map<TimelineItemUpdate>(timelineItemEdit);
      _tourTimelineCommandService.Add(timelineItemUpdate);
      return RedirectToRoute("admin-tour-timeline-items");
    }

    [HttpPost]
    public ActionResult Destroy(int tourId, int itemId)
    {
      try
      {
        _tourTimelineCommandService.Delete(itemId);
      }
      catch (DbUpdateException dbex)
      {
        Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
        TempData [ "Error" ] = "This timeline item could not be deleted.";
        return RedirectToRoute("admin-tour-timeline-item-edit", new { tourId, itemId });
      }
      return RedirectToRoute("admin-tour-timeline-items");
    }

    [HttpPost]
    public ActionResult Sort(int tourId, IList<TourTimelineItemPosition> positions)
    {
      var timelineItems = Mapper.Map<IEnumerable<TourTimelineItem>>(positions);
      _tourTimelineCommandService.UpdatePositions(tourId, timelineItems);
      return Json(positions);
    }
  }
}