using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Britespokes.Services.Perspectives
{
    public class PerspectiveService : IPerspectiveService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tour> _tourRepository;
        private readonly IRepository<PerspectivePost> _perspectivepostRepository;

        public PerspectiveService(IRepository<Category> categoryRepository, IRepository<PerspectivePost> perspectivepostRepository, IRepository<Tour> tourRepository)
        {
            _categoryRepository = categoryRepository;
            _perspectivepostRepository = perspectivepostRepository;
            _tourRepository = tourRepository;
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


        public IQueryable<PerspectivePost> LatestPost()
        {
            var latestpost = from PerspectivePosts in _perspectivepostRepository.All
                             where PerspectivePosts.IsPublished==true
                             group PerspectivePosts by new
                             {
                                 PerspectivePosts.TourId
                             } into g
                             select new
                             {
                                 Id = (int?)g.Max(p => p.Id),
                                 UpdatedAt = (DateTime?)g.Max(p => p.UpdatedAt),
                             };

           var latestpostq = _perspectivepostRepository.All.Where(z => latestpost.Select(x => x.Id).Contains(z.Id));
            return latestpostq;
             
        }

        public IQueryable<Tour> AllTours()
        {
            return _tourRepository.All;
        }

        public IQueryable<PerspectivePost> PostByTour(string tourpermalink)
        {

            var tour = _tourRepository.FindBy(t => t.Permalink == tourpermalink).ToList();

            var perspectiveposts = tour.FirstOrDefault().PerspectivePosts.Where(z=>z.IsPublished==true).AsQueryable();
            
            return perspectiveposts;

        }

        public IQueryable<PerspectivePost> FindPerspectivePost(string permalink)
        {
            return _perspectivepostRepository.All.Where(x => x.Permalink == permalink );
        }

        public IQueryable<Category> FindCategoryByPermalink(string tourpermalink)
        {
            return _categoryRepository.All.Where(x => x.Permalink == tourpermalink);
        }


    }
}
