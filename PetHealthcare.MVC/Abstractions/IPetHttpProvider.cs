using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IPetHttpProvider
    {
        Task<(bool IsSuccess, IEnumerable<PetDTO>? Pets, string? ErrorMessage)> GetAllPetsAsync(string token = "");
        Task<(bool IsSuccess, PetDTO? Pet, string? ErrorMessage)> GetPetAsync(int id, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> AddPetAsync(PetDTO petDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> EditPetAsync(int id, PetDTO petDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> DeletePetAsync(int id, string token = "");
    }
}
