namespace MagicShortener.Logic.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }
        string UserLogin { get; }
    }
}
