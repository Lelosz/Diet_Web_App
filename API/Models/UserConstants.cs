namespace WebAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() {Username = "Michael", EmailAdress = "michael@gmail.com", Password = "123", Role = "Administrator"},
            new UserModel() {Username = "Andrzej", EmailAdress = "andrzej@gmail.com", Password = "123", Role = "LoggedInUser"},
        };
    }
}
