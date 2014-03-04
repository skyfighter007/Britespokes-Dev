namespace Britespokes.Core.Domain
{
  public class Experience : Entity
  {
    public int TourId { get; set; }
    public virtual Tour Tour { get; set; }

    public int Position { get; set; }
  }
}