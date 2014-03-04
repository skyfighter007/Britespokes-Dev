using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britespokes.Web.Areas.Admin.Models.SubCategories
{
    public class SubCategoriesIndex
    {
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public int Count { get; set; }
    }
}