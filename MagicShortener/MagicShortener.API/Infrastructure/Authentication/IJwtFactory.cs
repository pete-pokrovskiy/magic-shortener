using System.Security.Claims;
using System.Threading.Tasks;

namespace MagicShortener.API.Infrastructure.Authentication
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string id);
    }
}
