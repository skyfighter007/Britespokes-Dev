using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britespokes.Web.Areas.Admin.Models.Perspectives
{
    public class PerspectivesIndex
    {
        public IEnumerable<PerspectivePost> Perspectives { get; set; }
        public int Count { get; set; }
    }
}