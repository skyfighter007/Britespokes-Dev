using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Experiences
{
  public class ExperiencesIndex
  {
    private readonly string[] _colors = { "blue", "orange", "green", "red" };

    public int Count { get; set; }
    public IEnumerable<ExperiencesIndexItem> Experiences { get; set; }
    public string[] Colors { get { return _colors; }}
    public string CategoryName { get; set; }
    public string CategoryPermalink { get; set; }
    public List<SubCategory> subcategory { get; set; }
    public List<Tour> Tours { get; set; }

  }

  public class ExperiencesIndexItem
  {
    public int Id { get; set; }
    public int TourId { get; set; }
    public string Permalink { get; set; }
    public int Position { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string ThumbnailImageAltText { get; set; }
    public string ThumbnailCaption { get; set; }
    public string JourneyDescription { get; set; }
  }
}