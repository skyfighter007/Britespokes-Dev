using Britespokes.Services.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Britespokes.Services.Admin.SubCategories
{
    public interface ISubCategoryCommandService
    {
        void Add(SubCategoryUpdate subcategoryUpdate);
        void Update(SubCategoryUpdate subcategoryUpdate);
        void Delete(int subcategoryid);
    }
}
