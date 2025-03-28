using SocialMedia.Models;

namespace SocialMedia.Repositories.Interfaces
{
    public interface ILogingToken
    {
        string GenerateJwtToken(User user);
    }
}
