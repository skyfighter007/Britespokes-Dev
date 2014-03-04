using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.SubCategories
{
    public interface ISubCategoryQueryService
    {
        IQueryable<Tour> FindTours(int CategoryId);
        IEnumerable<Category> Categories();
        IQueryable<SubCategory> SubCategories();
        bool IsCodeUnique(string code, int? tourId = null);
        IQueryable<Category> All();
        IQueryable<Tour> Tours();
        SubCategory FindSubCategory(int Id);

       
    }
}
