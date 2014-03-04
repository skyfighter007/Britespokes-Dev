using Britespokes.Core.Domain;
using System.Linq;

namespace Britespokes.Services.Categories
{
    public interface ICategoryService
    {
        IQueryable<Category> All();
        IQueryable<Category> LatestPost();
        Category FindCategory(int Id);
        bool IsCodeUnique(string GiftCode, int? GiftCardId = null);
    }
}
