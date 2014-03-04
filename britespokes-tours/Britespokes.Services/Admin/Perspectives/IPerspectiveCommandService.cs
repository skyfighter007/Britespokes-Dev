using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Perspectives
{
    public interface IPerspectiveCommandService
    {
        void Add(PerspectivePostUpdate tourUpdate);
        void Update(PerspectivePostUpdate tourUpdate);
        void Delete(int id);
    }
}