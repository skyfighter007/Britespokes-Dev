using Britespokes.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Britespokes.Services.Admin.Perspectives
{
    public interface IPerspectiveQueryService
    {
        IQueryable<PerspectivePost> All();
        PerspectivePost FindPerspectivePost(int Id);
        IEnumerable<Category> Categories();
    }
}
