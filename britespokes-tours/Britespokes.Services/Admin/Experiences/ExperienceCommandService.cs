using System.Collections.Generic;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Experiences
{
  public class ExperienceCommandService : IExperienceCommandService
  {
    private readonly IRepository<Experience> _experienceRepo;

    public ExperienceCommandService(IRepository<Experience> experienceRepo)
    {
      _experienceRepo = experienceRepo;
    }

    public void UpdatePositions(IEnumerable<Experience> experiences)
    {
      foreach (var experience in experiences)
      {
        _experienceRepo.Update(experience);
      }
    }

    public void Delete(int experienceId)
    {
      _experienceRepo.Delete(experienceId);
    }
  }
}