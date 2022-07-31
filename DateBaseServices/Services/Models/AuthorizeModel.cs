namespace DateBaseServices.Services.Models
{
    public class AuthorizeModel
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
    }
}