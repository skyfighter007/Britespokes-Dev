using System.Web.Mvc;
using Britespokes.Services.Admin.Dashboard;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class DashboardController : AdminBaseController
  {
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
      _dashboardService = dashboardService;
    }

    public ActionResult Index()
    {
      var mainStats = _dashboardService.MainStats();
      return View(mainStats);
    }
  }
}