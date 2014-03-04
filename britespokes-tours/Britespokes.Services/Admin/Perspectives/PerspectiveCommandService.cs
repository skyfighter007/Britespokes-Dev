using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.Perspectives
{
    public class PerspectiveCommandService : IPerspectiveCommandService
    {
        private readonly IRepository<PerspectivePost> _perspectiveRepo;
        private readonly IRepository<Category> _categoryRepo;

        public PerspectiveCommandService(IRepository<PerspectivePost> perspective, IRepository<Category> categoryRepo)
        {
            _perspectiveRepo = perspective;
            _categoryRepo = categoryRepo;

        }

        public void Add(PerspectivePostUpdate perspectivePostUpdate)
        {
            //var categories = _categoryRepo.All.Where(a => perspectivePostUpdate.SelectedCategories.Contains(a.Id));
            var perspectivePost = new PerspectivePost
              {
                  Headline = perspectivePostUpdate.Headline,
                  BodyContent = perspectivePostUpdate.BodyContent,
                  ThumbnailImageURL = perspectivePostUpdate.ThumbnailImageURL,
                  ThumbnailImageAltText = perspectivePostUpdate.ThumbnailImageAltText,
                  Image1URL = perspectivePostUpdate.Image1URL,
                  Image1AltText = perspectivePostUpdate.Image1AltText,
                  Image1Caption = perspectivePostUpdate.Image1Caption,
                  Image2URL = perspectivePostUpdate.Image2URL,
                  Image2AltText = perspectivePostUpdate.Image2AltText,
                  Image2Caption = perspectivePostUpdate.Image2Caption,

                  Image3URL = perspectivePostUpdate.Image3URL,
                  Image3AltText = perspectivePostUpdate.Image3AltText,
                  Image3Caption = perspectivePostUpdate.Image3Caption,
                  Image4URL = perspectivePostUpdate.Image4URL,
                  Image4AltText = perspectivePostUpdate.Image4AltText,
                  Image4Caption = perspectivePostUpdate.Image4Caption,

                  Permalink = perspectivePostUpdate.Permalink,
                  //FocusKeyword = perspectivePostUpdate.FocusKeyword,
                  //MetaDescription = perspectivePostUpdate.MetaDescription,
                  IsPublished = perspectivePostUpdate.IsPublished,
                  UserId = perspectivePostUpdate.UserId,
                  //SEOTitle = perspectivePostUpdate.SEOTitle,
                  TourId = perspectivePostUpdate.TourId,
                  UpdatedAt = DateTime.Now,
                  PostedOn = DateTime.Now
              };

            SEOTool SEOTool = new Core.Domain.SEOTool();
            SEOTool.FocusKeyword = perspectivePostUpdate.FocusKeyword;
            SEOTool.MetaDescription = perspectivePostUpdate.MetaDescription;
            SEOTool.SEOTitle = perspectivePostUpdate.SEOTitle;
            perspectivePost.SEOTools = new List<Core.Domain.SEOTool>();
            perspectivePost.SEOTools.Add(SEOTool);

            _perspectiveRepo.Add(perspectivePost);
        }



        public void Update(PerspectivePostUpdate PerspectivePost)
        {
            // var perspectivePostsCategories = _categoryRepo.All.Where(a => PerspectivePost.SelectedCategories.Contains(a.Id));
            var perspectivePost = _perspectiveRepo.Find(PerspectivePost.Id);

            perspectivePost.Headline = PerspectivePost.Headline;
            perspectivePost.BodyContent = PerspectivePost.BodyContent;
            perspectivePost.ThumbnailImageURL = PerspectivePost.ThumbnailImageURL;
            perspectivePost.ThumbnailImageAltText = PerspectivePost.ThumbnailImageAltText;
            perspectivePost.Image1URL = PerspectivePost.Image1URL;
            perspectivePost.Image1AltText = PerspectivePost.Image1AltText;
            perspectivePost.Image1Caption = PerspectivePost.Image1Caption;
            perspectivePost.Image2URL = PerspectivePost.Image2URL;
            perspectivePost.Image2AltText = PerspectivePost.Image2AltText;
            perspectivePost.Image2Caption = PerspectivePost.Image2Caption;
            perspectivePost.Image3URL = PerspectivePost.Image3URL;
            perspectivePost.Image3AltText = PerspectivePost.Image3AltText;
            perspectivePost.Image3Caption = PerspectivePost.Image3Caption;
            perspectivePost.Image4URL = PerspectivePost.Image4URL;
            perspectivePost.Image4AltText = PerspectivePost.Image4AltText;
            perspectivePost.Image4Caption = PerspectivePost.Image4Caption;
            perspectivePost.Permalink = PerspectivePost.Permalink;
            //perspectivePost.FocusKeyword = PerspectivePost.FocusKeyword;
            //perspectivePost.MetaDescription = PerspectivePost.MetaDescription;
            perspectivePost.IsPublished = PerspectivePost.IsPublished;
            //perspectivePost.SEOTitle = PerspectivePost.SEOTitle;
            perspectivePost.UpdatedAt = DateTime.Now;
            perspectivePost.TourId = PerspectivePost.TourId;

            SEOTool SEOTool = null;

            if (perspectivePost.SEOTools != null && perspectivePost.SEOTools.Count() > 0)
                SEOTool = perspectivePost.SEOTools.First();
            else
            {
                SEOTool = new Core.Domain.SEOTool();
                perspectivePost.SEOTools = new List<Core.Domain.SEOTool>();
                perspectivePost.SEOTools.Add(SEOTool);
            }

            SEOTool.FocusKeyword = PerspectivePost.FocusKeyword;
            SEOTool.MetaDescription = PerspectivePost.MetaDescription;
            SEOTool.SEOTitle = PerspectivePost.SEOTitle;
            
            
            _perspectiveRepo.Update(perspectivePost);

        }


        public void Delete(int perspectiveid)
        {
            _perspectiveRepo.Delete(perspectiveid);
        }

    }
}