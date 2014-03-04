using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.SEOToolStaticPages
{
    public class SEOToolStaticPageCommandService : ISEOToolStaticPageCommandService
    {
        private readonly IRepository<SEOToolStaticPage> _seoToolRepo;

        public SEOToolStaticPageCommandService(IRepository<SEOToolStaticPage> seoToolRepo)
        {
            _seoToolRepo = seoToolRepo;
        }

        public void Add(SEOToolStaticPageUpdate seoToolUpdate)
        {
            var seoTool = new SEOToolStaticPage
              {
                  FocusKeyword = seoToolUpdate.FocusKeyword,
                  MetaDescription = seoToolUpdate.MetaDescription,
                  SEOTitle = seoToolUpdate.SEOTitle,
                  Permalink = seoToolUpdate.Permalink
              };
            _seoToolRepo.Add(seoTool);
        }

        public void Update(SEOToolStaticPageUpdate seoToolUpdate)
        {
            var seoTool = _seoToolRepo.Find(seoToolUpdate.Id);

            seoTool.FocusKeyword = seoToolUpdate.FocusKeyword;
            seoTool.MetaDescription = seoToolUpdate.MetaDescription;
            seoTool.SEOTitle = seoToolUpdate.SEOTitle;
            seoTool.Permalink = seoToolUpdate.Permalink;

            _seoToolRepo.Update(seoTool);
        }

        public void Delete(int seoToolId)
        {
            _seoToolRepo.Delete(seoToolId);
        }

    }
}