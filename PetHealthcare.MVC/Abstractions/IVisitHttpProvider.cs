using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IVisitHttpProvider
    {
        Task<(bool IsSuccess, IEnumerable<VisitDTO>? Visits, string? ErrorMessage)> GetAllVisitsAsync(string token = "");
        Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage)> GetAllVisitAggregatesAsync(string token = "");
        Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage)> GetAllVisitAggregatesByPetAsync(int id, string token = "");
        Task<(bool IsSuccess, VisitDTO? Visit, string? ErrorMessage)> GetVisitAsync(int id, string token = "");
        Task<(bool IsSuccess, VisitAggregateDTO? VisitAggregate, string? ErrorMessage)> GetVisitAggregateAsync(int id, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> AddVisitAsync(VisitDTO visitDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> EditVisitAsync(int id, VisitDTO visitDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteVisitAsync(int id, string token = "");
    }
}
