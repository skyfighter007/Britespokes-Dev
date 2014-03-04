using Britespokes.Core.Domain;
using System.Linq;
using System.Collections.Generic;

namespace Britespokes.Services.Comments
{
    public interface ICommentService
    {
        IQueryable<Comment> All();
        //IQueryable<Comment> FindComment();
    }
}
