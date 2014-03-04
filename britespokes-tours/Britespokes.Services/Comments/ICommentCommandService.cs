using Britespokes.Core.Domain;

namespace Britespokes.Services.Admin.Perspectives
{
    public interface ICommentCommandService
    {
        void Add(CommentUpdate commentUpdate);
        void Update(CommentUpdate commentUpdate);
        void Delete(int id);
    }
}