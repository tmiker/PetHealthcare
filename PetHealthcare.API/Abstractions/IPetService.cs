using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IPetService
    {
        Task<(bool IsSuccess, IEnumerable<PetDTO>? Pets, string? ErrorMessage)> GetAllPetsAsync(string? userId = null);
        Task<(bool IsSuccess, PetDTO? Pet, string? ErrorMessage)> GetPetAsync(int id, string? userId = null);
        Task<(bool IsSuccess, string? ErrorMessage)> AddPetAsync(PetDTO petDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> EditPetAsync(int id, PetDTO petDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> DeletePetAsync(int id);
    }
}
