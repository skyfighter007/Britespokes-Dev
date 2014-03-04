using System.ComponentModel.DataAnnotations;

namespace Britespokes.Web.Areas.Admin.Models.Timeline
{
  public class TimelineItemEdit
  {
    public int TourId { get; set; }
    public string TourCode { get; set; }
    public string TourName { get; set; }
    public string TourPermalink { get; set; }

    public int Id { get; set; }
    [Required]
    [Display(Name = "Image")]
    public string ImageUrl { get; set; }
    [Required]
    public string Caption { get; set; }
  }
}