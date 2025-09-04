using System.Security.Claims;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IClaimsDecoder
    {
        List<Claim> GetClaims(string token);
    }
}
