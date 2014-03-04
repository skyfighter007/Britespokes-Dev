using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Britespokes.Services.Admin.SEOToolStaticPages
{
    public class SEOToolStaticPageQueryService : ISEOToolStaticPageQueryService
    {
        private readonly IRepository<SEOToolStaticPage> _seoToolStaticServiceRepository;

        public SEOToolStaticPageQueryService(IRepository<SEOToolStaticPage> seoToolStaticServiceyRepository)
        {
            _seoToolStaticServiceRepository = seoToolStaticServiceyRepository;
           
        }

        public IQueryable<SEOToolStaticPage> All()
        {
            return _seoToolStaticServiceRepository.All;
        }

        public SEOToolStaticPage FindSEOToolStaticPage(int Id)
        {
            return _seoToolStaticServiceRepository.Find(Id);
        }

        public SEOToolStaticPage FindSEOToolStaticPageByPermalink(string permalink)
        {
            return _seoToolStaticServiceRepository.All.Where(x => x.Permalink == permalink).FirstOrDefault();
        }

    }
}
