using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Organizations
{
  public interface IOrganizationService
  {
    IQueryable<Organization> Organizations();
    Organization FindOrganization(int organizationId);
    void Update(Organization organization);
    void UpdateEmailDomains(Organization organization, string emailDomainList);
    int UserCount(int organizationId);
    int TourCount(int organizationId);
    void Add(Organization organization);
    void Remove(int organizationId);
  }
}
