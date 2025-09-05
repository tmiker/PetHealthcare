using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IHealthSummaryService
    {
        Task<HealthSummaryDTO> GetPetHealthSummaryAsync(int id, string token = "");
    }
}
