using System.Data.Entity;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Experiences;
using Britespokes.Services.Categories;

namespace Britespokes.Services.Experiences
{
  public class ExperienceQueryService : IExperienceQueryService
  {
    private readonly IRepository<Experience> _experienceRepo;
    private readonly IRepository<Category> _categoryRepository;
    public ExperienceQueryService(IRepository<Experience> experienceRepo, IRepository<Category> categoryService)
    {
      _experienceRepo = experienceRepo;
      _categoryRepository = categoryService;
    }

    public IQueryable<Experience> Experiences(string permalink)
    {
       Category _category = _categoryRepository.All.Where(v => v.Permalink == permalink).First();

      return _experienceRepo.All
        .Include("Tour.TourMeta")
        .Where(t => t.Tour.TourMeta != null)
        .Where(t => t.Tour.IsPublished == true)
        .Where(t => t.Tour.Categories.Where(c=> c.Id == _category.Id).Any())
        .OrderBy(e => e.Position);
    }

    public IQueryable<Experience> Experiences()
    {
         return _experienceRepo.All
          .Include("Tour.TourMeta")
          .Where(t => t.Tour.TourMeta != null)
          .Where(t => t.Tour.IsPublished == true)
          .OrderBy(e => e.Position);
    }
  }
}