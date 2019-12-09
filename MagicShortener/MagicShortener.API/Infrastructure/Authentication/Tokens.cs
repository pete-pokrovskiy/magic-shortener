using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagicShortener.API.Infrastructure.Authentication
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JsonSerializerSettings serializerSettings)
        {
            return await jwtFactory.GenerateEncodedToken(userName, identity);
        }
    }
}
