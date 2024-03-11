namespace WebAPI.Models
{
    public class BlogReply
    {
        public int Id { get; set; }
        public String Author { get; set; }
        public String ReplyContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CommentId { get; set; }
        public BlogComment? Comment { get; set; }
        //public virtual BlogComment Comment { get; set; }
    }
}
