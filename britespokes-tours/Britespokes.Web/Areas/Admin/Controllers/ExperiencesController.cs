using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Experiences;
using Britespokes.Services.Admin.Tours;
using Britespokes.Services.Experiences;
using Britespokes.Web.Areas.Admin.Models.Experiences;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class ExperiencesController : AdminBaseController
  {
    private readonly IExperienceQueryService _experienceQueryService;
    private readonly IExperienceCommandService _experienceCommandService;
    private readonly ITourCommandService _tourCommandService;

    public ExperiencesController(IExperienceQueryService experienceQueryService, IExperienceCommandService experienceCommandService, ITourCommandService tourCommandService)
    {
      _experienceQueryService = experienceQueryService;
      _experienceCommandService = experienceCommandService;
      _tourCommandService = tourCommandService;
    }

    public ActionResult Index()
    {
      var experiences = _experienceQueryService.Experiences();
      return View(new ExperiencesIndex
      {
        Count = experiences.Count(),
        Experiences = Mapper.Map<List<ExperiencesIndexItem>>(experiences)
      });
    }

    [HttpPost]
    public ActionResult Create(int tourId, string redirectUrl)
    {
      TempData [ "Info" ] = "Experience has been created.";
      _tourCommandService.AddExperience(tourId);
      return Redirect(redirectUrl);
    }

    [HttpPost]
    public JsonResult Sort(IList<ExperiencePosition> positions)
    {
      var experiences = Mapper.Map<IEnumerable<Experience>>(positions);
      _experienceCommandService.UpdatePositions(experiences);
      return Json(positions);
    }

    [HttpPost]
    public ActionResult Destroy(int experienceId)
    {
      try
      {
        _experienceCommandService.Delete(experienceId);
      }
      catch (DbUpdateException dbex)
      {
        Elmah.ErrorSignal.FromCurrentContext().Raise(dbex);
        TempData [ "Error" ] = "This experience could not be deleted.";
      }
      return RedirectToRoute("admin-experiences");
    }
  }
}
