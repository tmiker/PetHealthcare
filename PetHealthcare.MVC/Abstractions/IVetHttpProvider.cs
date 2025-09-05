using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IVetHttpProvider
    {
        Task<(bool IsSuccess, IEnumerable<VetDTO>? Vets, string? ErrorMessage)> GetAllVetsAsync(string token = "");
        Task<(bool IsSuccess, VetDTO? Vet, string? ErrorMessage)> GetVetAsync(int id, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> AddVetAsync(VetDTO vetDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> EditVetAsync(int id, VetDTO vetDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteVetAsync(int id, string token = "");

    }
}
