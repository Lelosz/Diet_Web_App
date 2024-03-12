namespace WebAPI.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public String Author { get; set; }
        public String CommentContent { get; set; }
        public DateTime Created { get; set; }
        public int TotalLikes { get; set; } = 0;
        public int TotalDislikes { get; set; } = 0;

        public bool LikedByUser { get; set; } = false;
        public bool DislikedByUser { get; set; } = false;
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
        public int? BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
        public ICollection<BlogReply> Replies { get; } = new List<BlogReply>();
    }
}
