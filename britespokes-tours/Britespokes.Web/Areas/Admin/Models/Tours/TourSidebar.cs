namespace Britespokes.Web.Areas.Admin.Models.Tours
{
  public class TourSidebar
  {
    public TourSidebar()
    {
      ShowCreateExperienceButton = false;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Permalink { get; set; }
    public bool HasExperience { get; set; }
    public bool ShowCreateExperienceButton { get; set; }
  }
}