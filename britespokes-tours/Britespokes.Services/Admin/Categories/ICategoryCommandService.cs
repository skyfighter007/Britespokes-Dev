using Britespokes.Core.Domain;
using Britespokes.Services.Categories;

namespace Britespokes.Services.Admin.Categories
{
  public interface ICategoryCommandService
  {
      void Add(CategoryUpdate tourUpdate);
    void Update(CategoryUpdate tourUpdate);
    void Delete(int id);
  }
}