namespace Authentification.JWT.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId);
    }
}
