using AutoMapper;
using Britespokes.Services.Admin.Tours;
using Britespokes.Web.Areas.Api.Models.Tours;

namespace Britespokes.Web.Areas.Api.Controllers
{
  public class ToursController : ApiBaseController
  {
    private readonly ITourQueryService _tourQueryService;

    public ToursController(ITourQueryService tourQueryService)
    {
      _tourQueryService = tourQueryService;
    }

    // GET api/tours
    public ApiTours Get(string q = null)
    {
      return Mapper.Map<ApiTours>(_tourQueryService.Tours(q));
    }

    // GET api/tours/5
    public ApiTour Get(int id)
    {
      return Mapper.Map<ApiTour>(_tourQueryService.FindTour(id));
    }
  }
}