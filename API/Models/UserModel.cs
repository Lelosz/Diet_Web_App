namespace WebAPI.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAdress { get; set; }
        public string? Role { get; set; }

        public ICollection<CalculatorModel> Calculators { get; } = new List<CalculatorModel>();

        public ICollection<BlogPost> BlogPosts { get; } = new List<BlogPost>();
        public ICollection<BlogComment> BlogComments { get; } = new List<BlogComment>();
    }
}
