using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IVisitService
    {
        Task<(bool IsSuccess, IEnumerable<VisitDTO>? Visits, string? ErrorMessage)> GetAllVisitsAsync(string? userId = null);
        Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? Visits, string? ErrorMessage)> GetAllVisitAggregatesAsync(string? userId = null);
        Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? Visits, string? ErrorMessage)> GetAllVisitAggregatesByPetAsync(int id, string? userId = null);
        Task<(bool IsSuccess, VisitDTO? Visit, string? ErrorMessage)> GetVisitAsync(int id, string? userId = null);
        Task<(bool IsSuccess, VisitAggregateDTO? Visit, string? ErrorMessage)> GetVisitAggregateAsync(int id, string? userId = null);
        Task<(bool IsSuccess, string? ErrorMessage)> AddVisitAsync(VisitDTO visitDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> EditVisitAsync(int id, VisitDTO visitDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteVisitAsync(int id);
    }
}
