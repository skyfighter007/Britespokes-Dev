using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.SEOToolStaticPages
{
    public interface ISEOToolStaticPageQueryService
    {
        IQueryable<SEOToolStaticPage> All();
        SEOToolStaticPage FindSEOToolStaticPage(int Id);
        SEOToolStaticPage FindSEOToolStaticPageByPermalink(string permalink);
    }
}
