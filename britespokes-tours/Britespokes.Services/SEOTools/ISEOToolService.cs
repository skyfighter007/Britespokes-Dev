using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;

namespace Britespokes.Services.SEOTools
{
    public interface ISEOToolsService
    {
        IQueryable<SEOTool> All();
    }
}
