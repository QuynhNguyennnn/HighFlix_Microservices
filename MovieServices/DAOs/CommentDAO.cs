using MovieServices.Models;

namespace MovieServices.DAOs
{
    public class CommentDAO
    {
        public static List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var commentList = context.Comments.ToList();
                    foreach (var comment in commentList)
                    {
                        if (comment.IsActive)
                        {
                            comments.Add(comment);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return comments;
        }
        public static Comment CreateComment(Comment comment)
        {

            try
            {
                using (var context = new HighFlixV2Context())
                {
                    comment.IsActive = true;
                    comment.CommentedDate = DateTime.Now;
                    comment.MovieId = 2;
                    comment.UserId = 1;
                    context.Comments.Add(comment);
                    context.SaveChanges();
                    return comment;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Comment UpdateComment(Comment comment)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var _comment = context.Comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
                    if (_comment != null)
                    {
                        _comment.CommentContent = comment.CommentContent;
                        _comment.CommentedDate = DateTime.Now;

                        context.Entry(comment).CurrentValues.SetValues(_comment);
                        context.SaveChanges();
                        return _comment;
                    }
                    throw new Exception("Comment does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static Comment DeleteComment(int commentId)
        {
            try
            {
                using (var context = new HighFlixV2Context())
                {
                    var _comment = context.Comments.SingleOrDefault(c => c.CommentId == commentId);
                    if (_comment != null)
                    {
                        _comment.IsActive = false;

                        context.Entry(_comment).CurrentValues.SetValues(_comment);
                        context.SaveChanges();
                        return _comment;
                    }
                    throw new Exception("Comment does not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
