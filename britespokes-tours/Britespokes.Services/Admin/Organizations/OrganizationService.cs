using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Organizations
{
  public class OrganizationService : IOrganizationService
  {
    private readonly IRepository<Organization> _orgRepo;
    private readonly IRepository<EmailDomain> _emailDomainRepo;
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<Departure> _departureRepo;
    private readonly IRepository<Product> _productRepo;

    public OrganizationService(IRepository<Organization> orgRepo, IRepository<EmailDomain> emailDomainRepo, IRepository<User> userRepo, IRepository<Departure> departureRepo, IRepository<Product> productRepo)
    {
      _orgRepo = orgRepo;
      _emailDomainRepo = emailDomainRepo;
      _userRepo = userRepo;
      _departureRepo = departureRepo;
      _productRepo = productRepo;
    }

    public IQueryable<Organization> Organizations()
    {
      return _orgRepo.All;
    }

    public Organization FindOrganization(int organizationId)
    {
      return _orgRepo.Find(organizationId);
    }

    public void Update(Organization organization)
    {
      _orgRepo.Update(organization);
    }

    public void UpdateEmailDomains(Organization organization, string emailDomainList)
    {
      if (emailDomainList == null)
        emailDomainList = "";

      string[] emailDomains = emailDomainList
        .Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries)
        .Select(d => d.Trim().ToLower())
        .ToArray();

      UpdateEmailDomains(organization, emailDomains);
    }

    public int UserCount(int organizationId)
    {
      return _userRepo.All
                      .Include(u => u.Roles)
                      .Where(u => !u.Roles.Select(r => r.Name).Contains("Guest"))
                      .Count(u => u.OrganizationId == organizationId);
    }

    public int TourCount(int organizationId)
    {
      return _departureRepo.All
                           .Join(_productRepo.All, d => d.ProductId, p => p.Id,
                                 (d, p) => new {Departure = d, Product = p})
                           .Where(r => r.Product.Organizations.Select(o => o.Id).Contains(organizationId))
                           .Select(x => x.Departure.TourId).Distinct().Count();
    }

    public void Add(Organization organization)
    {
      _orgRepo.Add(organization);
    }

    public void Remove(int organizationId)
    {
      _orgRepo.Delete(organizationId);
    }

    public void UpdateEmailDomains(Organization organization, string[] domains)
    {
      if (organization.EmailDomains == null)
        organization.EmailDomains = new List<EmailDomain>();

      var existingEmailDomains = organization.EmailDomains.Select(d => d.Domain).ToArray();
      foreach (var emailDomain in domains.Where(emailDomain => !existingEmailDomains.Contains(emailDomain)))
      {
        organization.EmailDomains.Add(new EmailDomain
          {
            Domain = emailDomain,
            OrganizationId = organization.Id
          });
      }
      foreach (var emailDomain in organization.EmailDomains.ToArray().Where(emailDomain => !domains.Contains(emailDomain.Domain)))
      {
        _emailDomainRepo.Delete(emailDomain);
        organization.EmailDomains.Remove(emailDomain);
      }
      _orgRepo.Update(organization);
    }

  }
}