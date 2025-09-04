using Microsoft.IdentityModel.JsonWebTokens;
using PetHealthcare.MVC.Abstractions;
using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Services
{
    public class TokenStatusDecoder : ITokenStatusDecoder
    {
        public TokenStatusDTO GetJwtTokenStatus(string token)
        {
            JsonWebTokenHandler tokenHandler = new();
            var securityToken = tokenHandler.ReadToken(token) as JsonWebToken;
            TokenStatusDTO tokenStatusDTO = new()
            {
                CurrentUtcTime = DateTime.UtcNow,
                ValidFrom = securityToken!.ValidFrom.ToString(),
                ValidTo = securityToken!.ValidTo.ToString(),
                IssuedAt = securityToken!.IssuedAt.ToString()
            };
            return tokenStatusDTO;
        }
    }
}
