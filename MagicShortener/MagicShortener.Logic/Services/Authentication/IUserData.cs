namespace MagicShortener.Logic.Services.Authentication
{
    public interface IUserData
    {
        string Id { get; set; }
        string Login { get; set; }
    }
}