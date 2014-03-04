using System.Collections.Generic;
using System.Linq;
using Britespokes.Core.Domain;
using Britespokes.Core.Data;


namespace Britespokes.Services.Admin.SubCategories
{
 public   class SubCategoryQueryService :ISubCategoryQueryService
    {
     private readonly IRepository<Tour> _tourRepo;
     private readonly IRepository<SubCategory> _subcategoryRepository;
     private readonly IRepository<Category> _categorytRepository;

     public SubCategoryQueryService(IRepository<SubCategory> subcategoryRepository, IRepository<Category> categorytRepository, IRepository<Tour> tourRepo)
        {
            _tourRepo = tourRepo;
            _subcategoryRepository = subcategoryRepository;
            _categorytRepository = categorytRepository;
        }
     
        public IQueryable<SubCategory> All()
        {
            return _subcategoryRepository.All;
        }
        public SubCategory FindCategory(int Id)
        {
            return _subcategoryRepository.Find(Id);
        }

        public bool IsCodeUnique(string code, int? categoryId = null)
        {
            var query = _subcategoryRepository.FindBy(t => t.Code == code);
            if (categoryId.HasValue)
                query = query.Where(t => t.Id != categoryId);
            return query.Any();
        }


        

        public IQueryable<Tour> FindTours(int CategoryId)
        {
            return _tourRepo.All;
        }

        public IEnumerable<Category> Categories()
        {
            return _categorytRepository.All;
        }



        public IQueryable<SubCategory> SubCategories()
        {
            return _subcategoryRepository.All;
        }


        IQueryable<Category> ISubCategoryQueryService.All()
        {
            throw new System.NotImplementedException();
        }


        public IQueryable<Tour> Tours()
        {
            return _tourRepo.All;
        }


        public SubCategory FindSubCategory(int Id)
        {
            return _subcategoryRepository.Find(Id);
        }
    }
}
