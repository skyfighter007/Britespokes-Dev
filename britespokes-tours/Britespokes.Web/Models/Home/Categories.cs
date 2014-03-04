using System.Collections.Generic;
using Britespokes.Core.Domain;
using System;
using System.Web.Mvc;
using System.Globalization;

namespace Britespokes.Web.Models.Home
{
    public class CategoryShow
    {
        public List<Category> Categories { get; set; }
        private readonly string[] _colors = { "blue", "orange", "green", "red" };
        public string[] Colors { get { return _colors; } }

    }

}