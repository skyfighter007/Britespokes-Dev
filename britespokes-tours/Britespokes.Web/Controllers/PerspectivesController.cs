using System.Text;
using System.Web.Mvc;
using Britespokes.Web.App_Start;
using Britespokes.Services.Experiences;
using Britespokes.Core.Extensions;
using Britespokes.Services.Perspectives;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Britespokes.Web.Models.Perspectives;
using Britespokes.Web.Helpers;
using Britespokes.Core.Domain;
using Britespokes.Services.Admin.Perspectives;
using System;
using Britespokes.Services.Admin.Tours;
using Britespokes.Services.Admin.SEOToolStaticPages;


namespace Britespokes.Web.Controllers
{
    public class PerspectivesController : BritespokesController
    {
        private readonly IPerspectiveService _perspectiveService;
        private readonly IPerspectiveCommandService _perspectiveCommandService;
        private readonly ITourQueryService _tourQueryService;
        private readonly ICommentCommandService _commentCommandService;
        private readonly ISEOToolStaticPageQueryService _seoToolStaticPageQueryService;
        public PerspectivesController(IPerspectiveService perspectiveService, IPerspectiveCommandService perspectiveCommandService, ITourQueryService tourQueryService, ICommentCommandService commentCommandService, ISEOToolStaticPageQueryService seoToolStaticPageQueryService)
        {
            _perspectiveService = perspectiveService;
            _perspectiveCommandService = perspectiveCommandService;
            _tourQueryService = tourQueryService;
            _commentCommandService = commentCommandService;
            _seoToolStaticPageQueryService = seoToolStaticPageQueryService;
        }


        public ActionResult Perspectives()
        {
            SEOToolStaticPage SEOTool = _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink("perspectives");

            if (SEOTool != null)
            {
                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
            }

            var blog = _perspectiveService.LatestPost().ToList();
            var allTours = _perspectiveService.AllTours().ToList();

            return View(new LatestPerspectivePost
            {
                Tours = allTours,
                PerspectivePosts = blog,
                //Comment = new Models.Comments.CommentEdit()

            });
        }

        public ActionResult TourPerspectives(string tourpermalink, int page = 1)
        {
            SEOToolStaticPage SEOTool = _seoToolStaticPageQueryService.FindSEOToolStaticPageByPermalink(tourpermalink);

            if (SEOTool != null)
            {
                ViewBag.FocusKeyword = SEOTool.FocusKeyword;
                ViewBag.MetaDescription = SEOTool.MetaDescription;
                ViewBag.Title = SEOTool.SEOTitle;
            }

            var perspectiveposts = _perspectiveService.PostByTour(tourpermalink);

            var blog = perspectiveposts.Paged(page, DefaultPerPage, "UpdatedAt desc");
            var allTours = _perspectiveService.AllTours().ToList();

            var viewModel = new LatestPerspectivePost
            {
                Count = perspectiveposts.Count(),
                PerspectivePosts = blog.ToList(),
                Tours = allTours,
                selectedtourpermalink = tourpermalink,
                PagingInfo = new PagingInfo
               {
                   CurrentPage = page,
                   PerPage = DefaultPerPage,
                   TotalItems = perspectiveposts.Count(),
               }
                //,
                //Comment = new Models.Comments.CommentEdit()
            };
            return View(viewModel);
        }

        public ActionResult PerspectivePost(string tourpermalink, string permalink)
        {
            var post = _perspectiveService.FindPerspectivePost(permalink);
            var allTours = _perspectiveService.AllTours().ToList();

            SEOTool SEOTool = new SEOTool();

            if (post != null && post.FirstOrDefault() != null && post.First().SEOTools != null && post.First().SEOTools.Count > 0)
                SEOTool = post.First().SEOTools.First();


            ViewBag.FocusKeyword = SEOTool.FocusKeyword;
            ViewBag.MetaDescription = SEOTool.MetaDescription;
            ViewBag.Title = SEOTool.SEOTitle;

            return View(new LatestPerspectivePost
            {
                PerspectivePosts = post.ToList(),
                selectedtourpermalink = tourpermalink,
                Tours = allTours,
                FocusKeyword = SEOTool.FocusKeyword,
                MetaDescription = SEOTool.MetaDescription,
                SEOTitle = SEOTool.SEOTitle
                //Comment = new Models.Comments.CommentEdit()
            });
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PerspectivePost(LatestPerspectivePost latestPerspectivePost, int Id, string TourPermalink, string PerspectivePermalink)
        {
            if (ModelState.IsValid)
            {
                // var commentUpdate = Mapper.Map<CommentUpdate>(latestPerspectivePost.Comment);

                CommentUpdate commentUpdate = new CommentUpdate();
                commentUpdate.Name = latestPerspectivePost.Name;
                commentUpdate.Email = latestPerspectivePost.Email;
                commentUpdate.Content = latestPerspectivePost.Content;
                commentUpdate.PerspectivePostId = Id;
                commentUpdate.IsApproved = true;
                commentUpdate.PostedOn = DateTime.Now;

                _commentCommandService.Add(commentUpdate);
                //TempData["CommentSuccess"] = "Comment created successfully.";
                return RedirectToRoute("perspective-post", new { tourpermalink = TourPermalink, permalink = PerspectivePermalink });
            }

            var post = _perspectiveService.FindPerspectivePost(PerspectivePermalink);
            var allTours = _perspectiveService.AllTours().ToList();


            SEOTool SEOTool = new SEOTool();

            if (post != null && post.FirstOrDefault() != null && post.First().SEOTools != null && post.First().SEOTools.Count > 0)
                SEOTool = post.First().SEOTools.First();

            ViewBag.FocusKeyword = SEOTool.FocusKeyword;
            ViewBag.MetaDescription = SEOTool.MetaDescription;
            ViewBag.SEOTitle = SEOTool.SEOTitle;

            latestPerspectivePost.FocusKeyword = SEOTool.FocusKeyword;
            latestPerspectivePost.MetaDescription = SEOTool.MetaDescription;
            latestPerspectivePost.SEOTitle = SEOTool.SEOTitle;


            latestPerspectivePost.PerspectivePosts = post.ToList();
            latestPerspectivePost.selectedtourpermalink = TourPermalink;
            latestPerspectivePost.Tours = allTours;

            return View(latestPerspectivePost);

        }


        //public ActionResult CreateComment(LatestPerspectivePost latestPerspectivePost,)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var commentUpdate = Mapper.Map<CommentUpdate>(latestPerspectivePost.Comment);
        //        commentUpdate.PerspectivePostId = latestPerspectivePost.PerspectivePosts.FirstOrDefault().Id;
        //        _commentCommandService.Add(commentUpdate);

        //        TempData["Success"] = "Comment created successfully.";

        //        return PartialView("_comment", latestPerspectivePost.Comment);
        //    }

        //    return RedirectToRoute("perspective-post", new { tourpermalink = "", permalink = "" });
        //}


        //public ActionResult Create()
        //{
        //    var perspectiveEdit = new PerspectiveEdit
        //    {
        //        CategoryList = _perspectiveService.All(),
        //        CategoriesFromPost = new System.Collections.Generic.List<string>()
        //    };
        //    return View(perspectiveEdit);
        //}
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Create()
        {
            var perspectiveEdit = new PerspectiveEdit
            {
                /*Commented -> Category replaced by tours
                  CategoryList = _perspectiveQueryService.Categories(),
                  CategoriesFromPost = new System.Collections.Generic.List<string>()
                 */
                TourList = _tourQueryService.Tours()
            };
            return View(perspectiveEdit);
        }


        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(PerspectiveEdit perspectiveEdit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        perspectiveEdit.SelectedCategories = new System.Collections.Generic.List<int>();
        //        foreach (string s in perspectiveEdit.CategoriesFromPost)
        //            perspectiveEdit.SelectedCategories.Add(Convert.ToInt32(s));
        //        var perspectivePostUpdate = Mapper.Map<PerspectivePostUpdate>(perspectiveEdit);
        //        perspectivePostUpdate.UserId = UserContext.UserId;

        //        _perspectiveCommandService.Add(perspectivePostUpdate);
        //        //TempData["Info"] = "Perspective post updated";
        //        return RedirectToRoute("create-perspective");
        //    }
        //    if (perspectiveEdit.CategoriesFromPost == null)
        //        perspectiveEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
        //    perspectiveEdit.CategoryList = _perspectiveService.All();

        //    return View(perspectiveEdit);
        //}




        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PerspectiveEdit perspectiveEdit)
        {
            if (ModelState.IsValid)
            {
                /*Commented -> Category replaced by tours
                perspectiveEdit.SelectedCategories = new System.Collections.Generic.List<int>();
                foreach (string s in perspectiveEdit.CategoriesFromPost)
                    perspectiveEdit.SelectedCategories.Add(Convert.ToInt32(s));
                 */
                var perspectivePostUpdate = Mapper.Map<PerspectivePostUpdate>(perspectiveEdit);
                perspectivePostUpdate.UserId = UserContext.UserId;
                perspectivePostUpdate.Permalink = perspectivePostUpdate.Headline.Replace(" ", "_");
                _perspectiveCommandService.Add(perspectivePostUpdate);
                TempData["Success"] = "Blog created successfully.";


                var tour = _tourQueryService.FindTour(perspectiveEdit.TourId);

                TempData["PerspectivesPermalink"] = perspectivePostUpdate.Permalink;
                TempData["TourPermalink"] = tour.Permalink;


                return RedirectToRoute("create-perspective");


            }
            /*Commented -> Category replaced by tours
            if (perspectiveEdit.CategoriesFromPost == null)
                perspectiveEdit.CategoriesFromPost = new System.Collections.Generic.List<string>();
              perspectiveEdit.CategoryList = _perspectiveQueryService.Categories();
             */

            perspectiveEdit.TourList = _tourQueryService.Tours();

            return View(perspectiveEdit);
        }


    }
}
