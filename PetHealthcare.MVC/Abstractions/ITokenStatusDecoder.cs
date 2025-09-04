using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface ITokenStatusDecoder
    {
        TokenStatusDTO GetJwtTokenStatus(string token);
    }
}
