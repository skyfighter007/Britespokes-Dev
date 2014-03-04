using System.Web.Mvc;
using Britespokes.Core.Domain;
using Britespokes.Services.Users;
using Britespokes.Web.Infrastructure.Filters;
using Britespokes.Web.Mailers;
using Britespokes.Web.Models.Users;
using AutoMapper;

namespace Britespokes.Web.Controllers
{
  public class UsersController : BritespokesController
  {
    private readonly IUserService _userService;
    private readonly IUserMailer _userMailer;

    public UsersController(IUserService userService, IUserMailer userMailer)
    {
        _userService = userService;
      _userMailer = userMailer;
    }

  
   // //[ProductionRequireHttps]
    public ActionResult Edit()
    {
        var updateUserInput = _userService.FindUser(UserContext.UserId);
        //var updateUserInput = Mapper.Map<UpdateUserInput>(user);
        return View(updateUserInput);
    }

   [HttpPost]
    public ActionResult Edit(UpdateUserInput viewModel, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            _userService.UpdateUserAccount(viewModel, UserContext.UserId);
            return Redirect(returnUrl ?? "/");
        }
        return View(viewModel);
    }
  }
}