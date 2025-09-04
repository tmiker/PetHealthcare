using Microsoft.IdentityModel.JsonWebTokens;
using PetHealthcare.MVC.Abstractions;
using System.Security.Claims;

namespace PetHealthcare.MVC.Services
{
    public class ClaimsDecoder : IClaimsDecoder
    {
        public List<Claim> GetClaims(string token)
        {
            JsonWebTokenHandler tokenHandler = new();
            var securityToken = tokenHandler.ReadToken(token) as JsonWebToken;
            //string email = securityToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            List<Claim> claims = securityToken!.Claims.ToList();
            return claims;
        }
    }
}
