using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.SEOTools
{
    public class SEOToolCommandService : ISEOToolCommandService
    {
        private readonly IRepository<SEOTool> _seoToolRepo;

        public SEOToolCommandService(IRepository<SEOTool> seoToolRepo)
        {
            _seoToolRepo = seoToolRepo;
        }

        public void Add(SEOToolUpdate seoToolUpdate)
        {
            var seoTool = new SEOTool
              {
                  FocusKeyword = seoToolUpdate.FocusKeyword,
                  MetaDescription = seoToolUpdate.MetaDescription,
                  SEOTitle = seoToolUpdate.SEOTitle
              };
            _seoToolRepo.Add(seoTool);
        }

        public void Update(SEOToolUpdate seoToolUpdate)
        {
            var seoTool = _seoToolRepo.Find(seoToolUpdate.Id);

            seoTool.FocusKeyword = seoToolUpdate.FocusKeyword;
            seoTool.MetaDescription = seoToolUpdate.MetaDescription;
            seoTool.SEOTitle = seoToolUpdate.SEOTitle;

            _seoToolRepo.Update(seoTool);
        }

        public void Delete(int seoToolId)
        {
            _seoToolRepo.Delete(seoToolId);
        }

    }
}