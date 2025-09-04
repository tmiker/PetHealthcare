using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IVetService
    {
        Task<(bool IsSuccess, IEnumerable<VetDTO>? Vets, string? ErrorMessage)> GetAllVetsAsync(string? userId = null);
        Task<(bool IsSuccess, VetDTO? Vet, string? ErrorMessage)> GetVetAsync(int id, string? userId = null);
        Task<(bool IsSuccess, string? ErrorMessage)> AddVetAsync(VetDTO vetDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> EditVetAsync(int id, VetDTO vetDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteVetAsync(int id);
    }
}
