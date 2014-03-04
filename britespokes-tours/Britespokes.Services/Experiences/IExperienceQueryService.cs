using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Experiences
{
  public interface IExperienceQueryService
  {
    IQueryable<Experience> Experiences(string permalink);
    IQueryable<Experience> Experiences();
  }
}