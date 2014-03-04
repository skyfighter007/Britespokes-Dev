using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Tours
{
  public interface ITourCommandService
  {
    void Add(TourUpdate tourUpdate);
    void Update(TourUpdate tourUpdate);
    void UpdateMeta(TourMetaUpdate tourMetaUpdate);
    void Delete(int tourId);

    void AddExperience(Tour tour);
    void RemoveExperience(Tour tour);
    void AddExperience(int tourId);
    void RemoveExperience(int tourId);
  }
}