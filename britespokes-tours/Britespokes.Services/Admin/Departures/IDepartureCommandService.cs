using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Departures
{
  public interface IDepartureCommandService
  {
    Departure Add(DepartureUpdate departureUpdate);
    void Update(DepartureUpdate departureUpdate);
    void Delete(int productId);
  }
}
