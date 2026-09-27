using Microsoft.AspNetCore.Identity;

namespace NZworks.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
