namespace Core.Utilities.Security.JWT
{
    public class UserRefreshToken
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
