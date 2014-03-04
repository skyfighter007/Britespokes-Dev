using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Users
{
  public class RoleService : IRoleService
  {
    private readonly IRepository<Role> _roleRepository;

    public RoleService(IRepository<Role> roleRepository)
    {
      _roleRepository = roleRepository;
    }

    public Role Guest()
    {
      return FindByName("Guest");
    }

    public Role Customer()
    {
      return FindByName("Customer");
    }

    public Role Traveler()
    {
      return FindByName("Traveler");
    }

    public Role Admin()
    {
      return FindByName("Admin");
    }

    private Role FindByName(string name)
    {
      return _roleRepository.FindBy(r => r.Name == name).Single();
    }
  }
}