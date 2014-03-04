using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.SEOTools
{
    public interface ISEOToolCommandService
    {
        void Add(SEOToolUpdate commentUpdate);
        void Update(SEOToolUpdate commentUpdate);
        void Delete(int id);
    }
}