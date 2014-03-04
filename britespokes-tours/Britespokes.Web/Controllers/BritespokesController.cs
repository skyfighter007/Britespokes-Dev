using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Britespokes.Services.Authentication;
using Britespokes.Web.Models.Users;

namespace Britespokes.Web.Controllers
{
  public class BritespokesController : Controller
  {
    private UserContext _userContext;
    protected const int DefaultPerPage = 6;
    protected virtual new CustomPrincipal User
    {
      get { return HttpContext.User as CustomPrincipal; }
    }

    public UserContext UserContext
    {
      get { return _userContext ?? (_userContext = new UserContext(ViewBag.Organization, User)); }
    }

    protected string SecureProtocol
    {
      get { return Request.IsLocal ? "http" : "http"; }
    }

    protected object[] ModelStateErrorsForJson()
    {
      var errors = new List<object>();
      foreach (var field in ModelState.Keys)
      {
        var modelState = ModelState[field];
        if (modelState.Errors.Count > 0)
          errors.Add(new { Field = field, Error = modelState.Errors.First().ErrorMessage });
      }
      return errors.ToArray();
    }

    protected bool ViewExists(string name)
    {
      var result = ViewEngines.Engines.FindView(ControllerContext, name, null);
      return (result.View != null);
    }
  }
}