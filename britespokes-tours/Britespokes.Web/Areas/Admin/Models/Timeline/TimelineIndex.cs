using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Timeline
{
  public class TimelineIndex
  {
    public int TourId { get; set; }
    public string TourCode { get; set; }
    public string TourName { get; set; }
    public string TourPermalink { get; set; }
    public int Count { get; set; }
    public IEnumerable<TourTimelineItem> Items { get; set; }
  }
}