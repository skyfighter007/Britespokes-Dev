using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Organizations
{
  public class OrganizationService : IOrganizationService
  {
    private readonly IRepository<Organization> _repository;

    public OrganizationService(IRepository<Organization> repository)
    {
      _repository = repository;
    }

    public IQueryable<Organization> All()
    {
      return _repository.All;
    }

    public Organization Create(string name)
    {
      var org = new Organization { Name = name };
      _repository.Add(org);
      return org;
    }
  }
}