using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.SEOToolStaticPages
{
    public interface ISEOToolStaticPageCommandService
    {
        void Add(SEOToolStaticPageUpdate seoToolStaticPageUpdate);
        void Update(SEOToolStaticPageUpdate seoOToolStaticPageUpdate);
        void Delete(int id);
    }
}