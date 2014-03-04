using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Britespokes.Services.Admin.SubCategories
{
   public class SubCategoryUpdate
    {
       public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime? AvailableOn { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Category_Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ListImageURL { get; set; }
        public string ListImageAltText { get; set; }
        public string ListImageCaption { get; set; }
        public string SliderImageURL { get; set; }
        public string SliderImageAltText { get; set; }
        public string SliderImageCaption { get; set; }
        //public string SliderCaption { get; set; }
        //public string Description { get; set; }
        public string Permalink { get; set; }
        public bool Published { get; set; }
        public string FocusKeyword { get; set; }
        public string SEOTitle { get; set; }
        public string MetaDescription { get; set; }
        public List<int> SelectedTours { get; set; }
    }
}
