using Britespokes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Britespokes.Web.Areas.Admin.Models.Categories
{
    public class CategoriesIndex
    {
        public IEnumerable<Category> Categories { get; set; }
        public int Count { get; set; }
    }
}