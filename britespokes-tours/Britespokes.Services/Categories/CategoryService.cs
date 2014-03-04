using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Linq;

namespace Britespokes.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<PerspectivePost> _perspectivepostRepository;

        public CategoryService(IRepository<Category> categoryRepository,  IRepository<PerspectivePost> perspectivepostRepository)
        {
            _categoryRepository = categoryRepository;
            _perspectivepostRepository = perspectivepostRepository;
        }

        public IQueryable<Category> All()
        {
            return _categoryRepository.All;
        }
        public Category FindCategory(int Id)
        {
            return _categoryRepository.Find(Id);
        }

        public bool IsCodeUnique(string code, int? categoryId = null)
        {
            var query = _categoryRepository.FindBy(t => t.Code == code);
            if (categoryId.HasValue)
                query = query.Where(t => t.Id != categoryId);
            return query.Any();
        }


        public IQueryable<Category> LatestPost()
        {
           // var query = _categoryRepository.All.Where(m => m.PerspectivePosts.Any(x => x.IsPublished != false));
            var query = _categoryRepository.All;

            
            return query;
        }


    }
}
