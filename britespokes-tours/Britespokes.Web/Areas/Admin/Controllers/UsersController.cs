using System;
using System.Linq;
using System.Web.Mvc;
using Britespokes.Core.Extensions;
using Britespokes.Services.Admin.Users;
using Britespokes.Web.Areas.Admin.Models.Users;
using Britespokes.Web.Helpers;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  public class UsersController : AdminBaseController
  {
    private readonly IUserService _userService;
    private readonly IUserPruneService _userPruneService;

    public UsersController(IUserService userService, IUserPruneService userPruneService)
    {
      _userService = userService;
      _userPruneService = userPruneService;
    }

    public ActionResult Index(string role, int page = 1)
    {
      if (string.IsNullOrWhiteSpace(role)) role = UsersIndex.AllRoleName;
      var usersByStatus = _userService.UsersByRole(role);
      var total = usersByStatus.Count();
      var registeredCount = _userService.RegisteredCount();
      var users = usersByStatus.Paged(page, DefaultPerPage, "UpdatedAt desc");

      return View(new UsersIndex
        {
          Users = users,
          UserCount = registeredCount,
          GuestCount = _userService.GuestCount(),
          PagingInfo = new PagingInfo
          {
            CurrentPage = page,
            PerPage = DefaultPerPage,
            TotalItems = total
          },
          RoleFilter = role
        });
    }

    public ActionResult Show(int userId)
    {
      var user = _userService.FindUser(userId);
      return View(user);
    }

    [HttpPost]
    public ActionResult UpdateComment(int userId, string adminComment)
    {
      _userService.UpdateComment(userId, adminComment);
      return Redirect(Url.RouteUrl("admin-user-show", new { userId }));
    }

    [HttpPost]
    public ActionResult ToggleAdmin(int userId)
    {
      _userService.ToggleAdmin(userId);
      return RedirectToRoute("admin-user-show", new { userId });
    }

    public ActionResult PruneGuests()
    {
      var count = _userPruneService.PruneGuests(TimeSpan.FromHours(24));
      TempData [ "Info" ] = string.Format("{0} guests pruned", count);
      return RedirectToRoute("admin-users");
    }
  }
}