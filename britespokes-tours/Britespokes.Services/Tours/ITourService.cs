using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Tours
{
  public interface ITourService
  {
    IQueryable<Tour> All();
    Tour Find(int id);
    Tour FindByPermalink(string permalink);
    Tour FindByCode(string code);
    Departure FindDeparture(string code);
  }
}