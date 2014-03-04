using System.Web.Mvc;
using Britespokes.Web.Controllers;

namespace Britespokes.Web.Areas.Admin.Controllers
{
  [Authorize(Roles="Admin")]
  public class AdminBaseController : BritespokesController
  {
    protected const int DefaultPerPage = 25;
  }
}