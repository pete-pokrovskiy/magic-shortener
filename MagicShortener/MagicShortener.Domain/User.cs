namespace MagicShortener.Domain
{
    /// <summary>
    /// Пользователь API
    /// </summary>
    public class User
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
