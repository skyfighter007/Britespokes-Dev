using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Experiences
{
  public interface IExperienceCommandService
  {
    void UpdatePositions(IEnumerable<Experience> experiences);
    void Delete(int experienceId);
  }
}