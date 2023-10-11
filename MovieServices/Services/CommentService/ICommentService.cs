using MovieServices.Models;

namespace MovieServices.Services.CommentServices
{
    public interface ICommentService
    {
        List<Comment> GetComments();
        Comment CreateComment(Comment comment);
        Comment UpdateComment(Comment comment);
        Comment DeleteComment(int commentId);
    }
}
