using System;
using System.Collections.Generic;

namespace Britespokes.Core.Domain
{
    public class BlogPost : Entity
    {
        public BlogPost()
        {
            PostedOn = DateTime.UtcNow;
        }

        public string Title
        { get; set; }

        public string ShortDescription
        { get; set; }

        public string Description
        { get; set; }

        public DateTime? Modified
        { get; set; }

        public DateTime PostedOn
        { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
              

    }
}