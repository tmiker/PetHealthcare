using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Abstractions
{
    public interface ITokenProvider
    {
        Task<string> GetJwtToken(ApplicationUser user);

        string GetRefreshToken();
    }
}
