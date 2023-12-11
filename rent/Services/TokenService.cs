using System.Security.Cryptography;
using System;

namespace PersonApi.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateRefreshToken()
        {
            using var rng = new RNGCryptoServiceProvider();

            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }
    }
}
