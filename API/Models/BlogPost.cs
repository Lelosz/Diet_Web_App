namespace WebAPI.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
        public String PostContent { get; set; }
        public DateTime Created { get; set; }
        public bool Archivized { get; set; } = false;
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
        public ICollection<BlogComment> Comments { get; } = new List<BlogComment>();
    }
}
