using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Britespokes.Services.SEOTools
{
    public class SEOToolService : ISEOToolsService
    {
        private readonly IRepository<SEOTool> _seoToolServiceyRepository;

        public SEOToolService(IRepository<SEOTool> seoToolServiceyRepository)
        {
            _seoToolServiceyRepository = seoToolServiceyRepository;
           
        }

        public IQueryable<SEOTool> All()
        {
            return _seoToolServiceyRepository.All;
        }

    }
}
