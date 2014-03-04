using Britespokes.Core.Data;
using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Britespokes.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        //private readonly IRepository<Tour> _tourRepository;
        //private readonly IRepository<PerspectivePost> _perspectivepostRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public IQueryable<Comment> All()
        {
            return _commentRepository.All;
        }

        //public Comment FindComment(int Id)
        //{
        //    return _commentRepository.Find(Id);
        //}

    }
}
