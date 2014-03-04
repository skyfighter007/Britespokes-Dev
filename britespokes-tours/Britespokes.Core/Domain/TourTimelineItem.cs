namespace Britespokes.Core.Domain
{
  public class TourTimelineItem : Entity
  {
    public int TourId { get; set; }
    public virtual Tour Tour { get; set; }

    public string ImageUrl { get; set; }
    public string Caption { get; set; }
    public int Position { get; set; }
  }
}