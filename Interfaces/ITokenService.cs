using LoginAPI.Entities;

namespace LoginAPI.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
