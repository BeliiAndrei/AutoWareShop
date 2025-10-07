namespace WebShop.Domain.ResBook
{
    public class ResBook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string YouName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } // Изменил на string
        public string Info { get; set; }
    }
}