namespace APIS.DTOs.CommentDTOs.ResponseDto
{
    public class CommentReponse
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public string CommentContent { get; set; } = null!;

        public DateTime CommentedDate { get; set; }

        public int? Rating { get; set; }

    }
}
