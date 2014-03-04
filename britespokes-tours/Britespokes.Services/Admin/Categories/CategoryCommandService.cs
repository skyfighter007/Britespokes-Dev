using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using Britespokes.Services.Categories;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.Categories
{
    public class CategoryCommandService : ICategoryCommandService
    {
        private readonly IRepository<Category> _categoryRepo;


        public CategoryCommandService(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;

        }

        public void Add(CategoryUpdate categoryUpdate)
        {
            var category = new Category
              {
                  Code = categoryUpdate.Code,
                  Description = categoryUpdate.Description,
                  Name = categoryUpdate.Name,
                  Permalink = categoryUpdate.Permalink,
                  Published = categoryUpdate.Published,
                  ListImageCaption = categoryUpdate.ListImageCaption,
                  ListImageURL = categoryUpdate.ListImageURL,
                  ListImageAltText = categoryUpdate.ListImageAltText,
                  SliderImageURL = categoryUpdate.SliderImageURL,
                  SliderImageAltText = categoryUpdate.SliderImageAltText,
                  SliderImageCaption = categoryUpdate.SliderImageCaption
              };

            SEOTool SEOTool = null;

            if (category.SEOTools != null && category.SEOTools.Count() > 0)
                SEOTool = category.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                category.SEOTools = new List<Core.Domain.SEOTool>();
                category.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = categoryUpdate.FocusKeyword;
            SEOTool.MetaDescription = categoryUpdate.MetaDescription;
            SEOTool.SEOTitle = categoryUpdate.SEOTitle;


            _categoryRepo.Add(category);
        }

        public void Update(CategoryUpdate categoryUpdate)
        {
            var category = _categoryRepo.Find(categoryUpdate.Id);
            category.Code = categoryUpdate.Code;
            category.Description = categoryUpdate.Description;
            category.Name = categoryUpdate.Name;
            category.Permalink = categoryUpdate.Permalink;
            category.Published = categoryUpdate.Published;
            category.ListImageCaption = categoryUpdate.ListImageCaption;
            category.ListImageURL = categoryUpdate.ListImageURL;
            category.ListImageAltText = categoryUpdate.ListImageAltText;
            category.SliderImageURL = categoryUpdate.SliderImageURL;
            category.SliderImageAltText = categoryUpdate.SliderImageAltText;
            category.SliderImageCaption = categoryUpdate.SliderImageCaption;

            SEOTool SEOTool = null;

            if (category.SEOTools != null && category.SEOTools.Count() > 0)
                SEOTool = category.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                category.SEOTools = new List<Core.Domain.SEOTool>();
                category.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = categoryUpdate.FocusKeyword;
            SEOTool.MetaDescription = categoryUpdate.MetaDescription;
            SEOTool.SEOTitle = categoryUpdate.SEOTitle;

            _categoryRepo.Update(category);
        }


        public void Delete(int tourId)
        {
            var category = _categoryRepo.Find(tourId);
            _categoryRepo.Delete(tourId);
        }

    }
}