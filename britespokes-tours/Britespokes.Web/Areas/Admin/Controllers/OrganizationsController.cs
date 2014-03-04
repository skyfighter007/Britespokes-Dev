using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Organizations;
using Britespokes.Web.Areas.Admin.Models.Organizations;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class OrganizationsController : AdminBaseController
  {
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
      _organizationService = organizationService;
    }

    public ActionResult Index()
    {
      var orgs = _organizationService.Organizations();
      return View(new OrganizationsIndex
        {
          Count = orgs.Count(),
          Organizations = orgs
        });
    }

    public ActionResult Edit(int organizationId)
    {
      var organization = _organizationService.FindOrganization(organizationId);
      return View(GetOrganizationStats(Mapper.Map<OrganizationEdit>(organization)));
    }

    [HttpPost]
    public ActionResult Edit(OrganizationEdit orgEdit)
    {
      if (ModelState.IsValid)
      {
        var organization = _organizationService.FindOrganization(orgEdit.Id);
        Mapper.Map(orgEdit, organization);
        _organizationService.Update(organization);
        _organizationService.UpdateEmailDomains(organization, orgEdit.EmailDomainList);
        return RedirectToRoute("admin-organizations");
      }
      return View(GetOrganizationStats(orgEdit));
    }

    public ActionResult Create()
    {
      return View(new OrganizationEdit());
    }

    [HttpPost]
    public ActionResult Create(OrganizationEdit orgEdit)
    {
      if (ModelState.IsValid)
      {
        var organization = Mapper.Map<Organization>(orgEdit);
        _organizationService.Add(organization);
        _organizationService.UpdateEmailDomains(organization, orgEdit.EmailDomainList);
        return RedirectToRoute("admin-organizations");
      }
      return View(orgEdit);
    }

    [HttpPost]
    public ActionResult Destroy(int organizationId)
    {
      _organizationService.Remove(organizationId);
      return RedirectToRoute("admin-organizations");
    }

    private OrganizationEdit GetOrganizationStats(OrganizationEdit orgEdit)
    {
      orgEdit.UserCount = _organizationService.UserCount(orgEdit.Id);
      orgEdit.TourCount = _organizationService.TourCount(orgEdit.Id);
      return orgEdit;
    }
  }
}