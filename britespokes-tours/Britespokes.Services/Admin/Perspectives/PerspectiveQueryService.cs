using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Britespokes.Services.Admin.Perspectives
{
    public class PerspectiveQueryService : IPerspectiveQueryService
    {
        private readonly IRepository<PerspectivePost> _perspectiveRepository;
        private readonly IRepository<Category> _CategoryRepo;
        public PerspectiveQueryService(IRepository<PerspectivePost> perspectiveRepository, IRepository<Category> CategoryRep)
        {
            _perspectiveRepository = perspectiveRepository;
            _CategoryRepo = CategoryRep;
        }

        public IQueryable<PerspectivePost> All()
        {
            return _perspectiveRepository.All;
        }
        public PerspectivePost FindPerspectivePost(int Id)
        {
            return _perspectiveRepository.Find(Id);
        }

        public IEnumerable<Category> Categories()
        {
            return _CategoryRepo.All;
        }
    }
}
