using Microsoft.AspNetCore.Identity;

namespace ClothingStore.Repositories.Token
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, string roles);
    }
}
