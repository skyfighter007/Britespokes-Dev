using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britespokes.Web.Areas.Admin.Models.SEOToolStaticPages
{
    public class SEOToolStaticPagesIndex
    {
        public IEnumerable<SEOToolStaticPage> SEOToolStaticPages { get; set; }
        public int Count { get; set; }
    }
}