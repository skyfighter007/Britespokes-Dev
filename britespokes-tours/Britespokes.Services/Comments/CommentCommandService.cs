using System;
using System.Linq;
using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Collections.Generic;

namespace Britespokes.Services.Admin.Perspectives
{
    public class CommentCommandService : ICommentCommandService
    {
        private readonly IRepository<Comment> _commentRepo;

        public CommentCommandService(IRepository<Comment> comment)
        {
            _commentRepo = comment;
        }

        public void Add(CommentUpdate commentUpdate)
        {
            //var categories = _categoryRepo.All.Where(a => perspectivePostUpdate.SelectedCategories.Contains(a.Id));
            var comment = new Comment
              {
                  PerspectivePostId = commentUpdate.PerspectivePostId,
                  Name = commentUpdate.Name,
                  Email = commentUpdate.Email,
                  Content = commentUpdate.Content,
                  IsApproved = commentUpdate.IsApproved,
                  PostedOn = commentUpdate.PostedOn
                 
              };
            //perspectivePost.Categories = new System.Collections.Generic.List<Category>();
            //perspectivePost.Categories.AddRange(categories.ToList());
            _commentRepo.Add(comment);
        }

        public void Update(CommentUpdate PerspectivePost)
        {
           

        }

        public void Delete(int commentid)
        {
            _commentRepo.Delete(commentid);
        }

    }
}