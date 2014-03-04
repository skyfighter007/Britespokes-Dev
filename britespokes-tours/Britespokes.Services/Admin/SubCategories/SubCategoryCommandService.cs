using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Categories;
using System.Collections.Generic;


namespace Britespokes.Services.Admin.SubCategories
{
  public  class SubCategoryCommandService :ISubCategoryCommandService
    {
      private readonly IRepository<SubCategory> _subcategoryRepo;
       private readonly IRepository<Tour> _tourRepo;

      public SubCategoryCommandService(IRepository<SubCategory> subcategoryRepo,IRepository<Tour> tourRepo)
        {
            _subcategoryRepo = subcategoryRepo;
            _tourRepo = tourRepo;

        }

    
      

        public void Delete(int subcategoryid)
        {
            var category = _subcategoryRepo.Find(subcategoryid);
            _subcategoryRepo.Delete(subcategoryid);
        }

        public void Add(SubCategoryUpdate subcategoryUpdate)
        {
            var subcategory = new SubCategory
            {
                Code = subcategoryUpdate.Code,
                Description = subcategoryUpdate.Description,
                Name = subcategoryUpdate.Name,
                Permalink = subcategoryUpdate.Permalink,
                Published = subcategoryUpdate.Published,
                ListImageCaption = subcategoryUpdate.ListImageCaption,
                ListImageURL = subcategoryUpdate.ListImageURL,
                ListImageAltText = subcategoryUpdate.ListImageAltText,
                SliderImageURL = subcategoryUpdate.SliderImageURL,
                SliderImageAltText = subcategoryUpdate.SliderImageAltText,
                SliderImageCaption = subcategoryUpdate.SliderImageCaption,
                CategoriesId=subcategoryUpdate.Category_Id
            };

            SEOTool SEOTool = null;

            if (subcategory.SEOTools != null && subcategory.SEOTools.Count() > 0)
                SEOTool = subcategory.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                subcategory.SEOTools = new List<Core.Domain.SEOTool>();
                subcategory.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = subcategoryUpdate.FocusKeyword;
            SEOTool.MetaDescription = subcategoryUpdate.MetaDescription;
            SEOTool.SEOTitle = subcategoryUpdate.SEOTitle;
            var tours = _tourRepo.All.Where(a => subcategoryUpdate.SelectedTours.Contains(a.Id));
            subcategory.Tours = new System.Collections.Generic.List<Tour>();
            subcategory.Tours.AddRange(tours.ToList());
            _subcategoryRepo.Add(subcategory);
            
        }


        public void Update(SubCategoryUpdate subcategoryUpdate)
        {
            var subcategory = _subcategoryRepo.Find(subcategoryUpdate.Id);
            subcategory.Code = subcategoryUpdate.Code;
            subcategory.Description = subcategoryUpdate.Description;
            subcategory.Name = subcategoryUpdate.Name;
            subcategory.Permalink = subcategoryUpdate.Permalink;
            subcategory.Published = subcategoryUpdate.Published;
            subcategory.ListImageCaption = subcategoryUpdate.ListImageCaption;
            subcategory.ListImageURL = subcategoryUpdate.ListImageURL;
            subcategory.ListImageAltText = subcategoryUpdate.ListImageAltText;
            subcategory.SliderImageURL = subcategoryUpdate.SliderImageURL;
            subcategory.SliderImageAltText = subcategoryUpdate.SliderImageAltText;
            subcategory.SliderImageCaption = subcategoryUpdate.SliderImageCaption;
            subcategory.CategoriesId = subcategoryUpdate.Category_Id;

            SEOTool SEOTool = null;

            if (subcategory.SEOTools != null && subcategory.SEOTools.Count() > 0)
                SEOTool = subcategory.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                subcategory.SEOTools = new List<Core.Domain.SEOTool>();
                subcategory.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = subcategoryUpdate.FocusKeyword;
            SEOTool.MetaDescription = subcategoryUpdate.MetaDescription;
            SEOTool.SEOTitle = subcategoryUpdate.SEOTitle;


            List<Tour> existingTours = subcategory.Tours.ToList();
            foreach (Tour t in existingTours)
                subcategory.Tours.Remove(t);
            subcategory.Tours = new System.Collections.Generic.List<Tour>();
            var tourss = _tourRepo.All.Where(a => subcategoryUpdate.SelectedTours.Contains(a.Id));
            subcategory.Tours.AddRange(tourss);
            _subcategoryRepo.Update(subcategory);
        }
    }
}
