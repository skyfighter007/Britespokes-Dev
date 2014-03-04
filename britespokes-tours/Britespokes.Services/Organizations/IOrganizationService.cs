using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Organizations
{
  public interface IOrganizationService
  {
    IQueryable<Organization> All();
    Organization Create(string name);
  }
}