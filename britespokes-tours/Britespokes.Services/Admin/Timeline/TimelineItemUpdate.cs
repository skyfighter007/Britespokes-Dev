namespace Britespokes.Services.Admin.Timeline
{
  public class TimelineItemUpdate
  {
    public int Id { get; set; }
    public int TourId { get; set; }
    public string ImageUrl { get; set; }
    public string Caption { get; set; }
  }
}