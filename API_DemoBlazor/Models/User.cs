namespace API_DemoBlazor.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public bool IsAdmin { get; set; }
    }
}
