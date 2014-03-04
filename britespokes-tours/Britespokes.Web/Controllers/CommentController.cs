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
using Britespokes.Services.Comments;
using Britespokes.Web.Models.Comments;


namespace Britespokes.Web.Controllers
{
    public class CommentsController : BritespokesController
    {
        private readonly ICommentService _commentService;
        private readonly ICommentCommandService _commentCommandService;
        public CommentsController(ICommentService commentService, ICommentCommandService commentCommandService)
        {
            _commentService = commentService;
            _commentCommandService = commentCommandService;
        }

       
        public ActionResult Create()
        {
            var perspectiveEdit = new CommentEdit
            {
                /*Commented -> Category replaced by tours
                  CategoryList = _perspectiveQueryService.Categories(),
                  CategoriesFromPost = new System.Collections.Generic.List<string>()
                 */
                //TourList = _tourQueryService.Tours()
            };
            return View(perspectiveEdit);
        }


      



        //public ActionResult Create(LatestPerspectivePost latestPerspectivePost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var commentUpdate = Mapper.Map<CommentUpdate>(latestPerspectivePost.Comment);
        //        commentUpdate.PerspectivePostId = latestPerspectivePost.PerspectivePosts.FirstOrDefault().Id;
        //        _commentCommandService.Add(commentUpdate);

        //         TempData["Success"] = "Comment created successfully.";

        //         return PartialView("_comment",latestPerspectivePost.Comment);
        //    }

        //    return RedirectToRoute("perspective-post", new { tourpermalink = "", permalink = "" });
        //}

    }
}
