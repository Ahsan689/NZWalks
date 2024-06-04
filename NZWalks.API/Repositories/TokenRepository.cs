using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public string createJWTToken(IdentityUser user, List<string> roles)
        {
           
        }
    }
}
