using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;

namespace Britespokes.Services.Perspectives
{
    public interface IPerspectiveService
    {
        IQueryable<Category> All();
        IQueryable<PerspectivePost> LatestPost();
        Category FindCategory(int Id);
        bool IsCodeUnique(string GiftCode, int? GiftCardId = null);
        IQueryable<PerspectivePost> PostByTour(string tourpermalink);
        IQueryable<PerspectivePost> FindPerspectivePost(string permalink);
        IQueryable<Category> FindCategoryByPermalink(string tourpermalink);
        IQueryable<Tour> AllTours();
    }
}
