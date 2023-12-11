namespace PersonApi.Services
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
    }
}
