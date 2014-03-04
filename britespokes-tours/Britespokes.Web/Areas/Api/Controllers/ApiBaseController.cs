using System.Web.Http;

namespace Britespokes.Web.Areas.Api.Controllers
{
  [Authorize(Roles = "Admin")]
  public class ApiBaseController : ApiController
  {
  }
}