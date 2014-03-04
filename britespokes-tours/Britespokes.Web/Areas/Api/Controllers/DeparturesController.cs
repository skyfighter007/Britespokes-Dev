using AutoMapper;
using Britespokes.Services.Admin.Departures;
using Britespokes.Web.Areas.Api.Models.Departures;

namespace Britespokes.Web.Areas.Api.Controllers
{
  public class DeparturesController : ApiBaseController
  {
    private readonly IDepartureQueryService _departureQueryService;

    public DeparturesController(IDepartureQueryService departureQueryService)
    {
      _departureQueryService = departureQueryService;
    }

    // GET api/departures
    public ApiDepartures Get(int? tourId)
    {
      var departures = tourId.HasValue
        ? _departureQueryService.Departures(tourId.Value)
        : _departureQueryService.Departures();
      return Mapper.Map<ApiDepartures>(departures);
    }

    // GET api/departures/5
    public ApiDeparture Get(int id)
    {
      return Mapper.Map<ApiDeparture>(_departureQueryService.FindDeparture(id));
    }
  }
}