using System.Collections.Generic;
using Britespokes.Core.Domain;

namespace Britespokes.Web.Areas.Admin.Models.Tours
{
    public class ToursIndex
    {
        public IEnumerable<Tour> Tours { get; set; }
        public int Count { get; set; }
    }
}