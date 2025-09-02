namespace JWTAuthentication.Entities
{
    public class User
    {
        public int Id { get; set; }
        public  string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        public string Role { get; set; }
    }
}
