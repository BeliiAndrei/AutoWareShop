namespace WebShop.Models.User
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public decimal Balance { get; set; }
    }
}