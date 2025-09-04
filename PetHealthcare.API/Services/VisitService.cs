using AutoMapper;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class VisitService : IVisitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VisitService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<VisitDTO>? Visits, string? ErrorMessage)> GetAllVisitsAsync(string? userId = null)
        {
            IEnumerable<VisitDTO> visitDTOs = new List<VisitDTO>();
            IEnumerable<Visit> visits = new List<Visit>();
            if (userId == null)
            {
                visits = await _unitOfWork.Visits.GetAllAsync();
            }
            else
            {
                visits = await _unitOfWork.Visits.GetAllAsync(filter: v => v.OwnedBy == userId);
            }
            visitDTOs = _mapper.Map<IEnumerable<VisitDTO>>(visits);
            return (true, visitDTOs, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? Visits, string? ErrorMessage)> GetAllVisitAggregatesAsync(string? userId = null)
        {
            IEnumerable<VisitAggregateDTO> visitAggregateDTOs = new List<VisitAggregateDTO>();
            if (userId == null)
            {
                visitAggregateDTOs = from visit in await _unitOfWork.Visits.GetAllAsync(includeProperties: "PetNavigation,VetNavigation")
                                     select new VisitAggregateDTO()
                                     {
                                         OwnedBy = visit.OwnedBy,
                                         Id = visit.Id,
                                         PetId = visit.PetId,
                                         PetName = visit.PetNavigation!.Name,
                                         PetImageURL = visit.PetNavigation.ImageURL,
                                         VetId = visit.VetId,
                                         Doctor = visit.VetNavigation!.Doctor,
                                         Hospital = visit.VetNavigation.Hospital,
                                         DateOfVisit = visit.DateOfVisit,
                                         VisitType = visit.VisitType,
                                         Reason = visit.Reason,
                                         Weight = visit.Weight,
                                         Diagnosis = visit.Diagnosis,
                                         Prescriptions = visit.Prescriptions,
                                         Instructions = visit.Instructions,
                                         Notes = visit.Notes,
                                         Heartworm = visit.Heartworm,
                                         Bordatella = visit.Bordatella,
                                         Rabies = visit.Rabies,
                                         Da2ppv = visit.Da2ppv,
                                         Leptospirosis = visit.Leptospirosis,
                                         InfluenzaH3N2 = visit.InfluenzaH3N2,
                                         InfluenzaH3N8 = visit.InfluenzaH3N8,
                                         FecalTest = visit.FecalTest,
                                         OtherTests = visit.OtherTests
                                     };
            }
            else
            {
                visitAggregateDTOs = from visit in await _unitOfWork.Visits.GetAllAsync(filter: v => v.OwnedBy == userId, includeProperties: "PetNavigation,VetNavigation")
                                     select new VisitAggregateDTO()
                                     {
                                         OwnedBy = visit.OwnedBy,
                                         Id = visit.Id,
                                         PetId = visit.PetId,
                                         PetName = visit.PetNavigation!.Name,
                                         PetImageURL = visit.PetNavigation.ImageURL,
                                         VetId = visit.VetId,
                                         Doctor = visit.VetNavigation!.Doctor,
                                         Hospital = visit.VetNavigation.Hospital,
                                         DateOfVisit = visit.DateOfVisit,
                                         VisitType = visit.VisitType,
                                         Reason = visit.Reason,
                                         Weight = visit.Weight,
                                         Diagnosis = visit.Diagnosis,
                                         Prescriptions = visit.Prescriptions,
                                         Instructions = visit.Instructions,
                                         Notes = visit.Notes,
                                         Heartworm = visit.Heartworm,
                                         Bordatella = visit.Bordatella,
                                         Rabies = visit.Rabies,
                                         Da2ppv = visit.Da2ppv,
                                         Leptospirosis = visit.Leptospirosis,
                                         InfluenzaH3N2 = visit.InfluenzaH3N2,
                                         InfluenzaH3N8 = visit.InfluenzaH3N8,
                                         FecalTest = visit.FecalTest,
                                         OtherTests = visit.OtherTests
                                     };
            }
            return (true, visitAggregateDTOs, null);
        }

        public async Task<(bool IsSuccess, IEnumerable<VisitAggregateDTO>? Visits, string? ErrorMessage)> GetAllVisitAggregatesByPetAsync(int id, string? userId = null)
        {
            IEnumerable<VisitAggregateDTO> visitAggregateDTOs = new List<VisitAggregateDTO>();
            if (userId == null)
            {
                visitAggregateDTOs = from visit in await _unitOfWork.Visits.GetAllAsync(filter: v => v.PetId == id, includeProperties: "PetNavigation,VetNavigation")
                                     select new VisitAggregateDTO()
                                     {
                                         OwnedBy = visit.OwnedBy,
                                         Id = visit.Id,
                                         PetId = visit.PetId,
                                         PetName = visit.PetNavigation!.Name,
                                         PetImageURL = visit.PetNavigation.ImageURL,
                                         VetId = visit.VetId,
                                         Doctor = visit.VetNavigation!.Doctor,
                                         Hospital = visit.VetNavigation.Hospital,
                                         DateOfVisit = visit.DateOfVisit,
                                         VisitType = visit.VisitType,
                                         Reason = visit.Reason,
                                         Weight = visit.Weight,
                                         Diagnosis = visit.Diagnosis,
                                         Prescriptions = visit.Prescriptions,
                                         Instructions = visit.Instructions,
                                         Notes = visit.Notes,
                                         Heartworm = visit.Heartworm,
                                         Bordatella = visit.Bordatella,
                                         Rabies = visit.Rabies,
                                         Da2ppv = visit.Da2ppv,
                                         Leptospirosis = visit.Leptospirosis,
                                         InfluenzaH3N2 = visit.InfluenzaH3N2,
                                         InfluenzaH3N8 = visit.InfluenzaH3N8,
                                         FecalTest = visit.FecalTest,
                                         OtherTests = visit.OtherTests
                                     };
            }
            else
            {
                visitAggregateDTOs = from visit in await _unitOfWork.Visits.GetAllAsync(filter: v => v.PetId == id && v.OwnedBy == userId, includeProperties: "PetNavigation,VetNavigation")
                                     select new VisitAggregateDTO()
                                     {
                                         OwnedBy = visit.OwnedBy,
                                         Id = visit.Id,
                                         PetId = visit.PetId,
                                         PetName = visit.PetNavigation!.Name,
                                         PetImageURL = visit.PetNavigation.ImageURL,
                                         VetId = visit.VetId,
                                         Doctor = visit.VetNavigation!.Doctor,
                                         Hospital = visit.VetNavigation.Hospital,
                                         DateOfVisit = visit.DateOfVisit,
                                         VisitType = visit.VisitType,
                                         Reason = visit.Reason,
                                         Weight = visit.Weight,
                                         Diagnosis = visit.Diagnosis,
                                         Prescriptions = visit.Prescriptions,
                                         Instructions = visit.Instructions,
                                         Notes = visit.Notes,
                                         Heartworm = visit.Heartworm,
                                         Bordatella = visit.Bordatella,
                                         Rabies = visit.Rabies,
                                         Da2ppv = visit.Da2ppv,
                                         Leptospirosis = visit.Leptospirosis,
                                         InfluenzaH3N2 = visit.InfluenzaH3N2,
                                         InfluenzaH3N8 = visit.InfluenzaH3N8,
                                         FecalTest = visit.FecalTest,
                                         OtherTests = visit.OtherTests
                                     };
            }
            return (true, visitAggregateDTOs, null);
        }

        public async Task<(bool IsSuccess, VisitDTO? Visit, string? ErrorMessage)> GetVisitAsync(int id, string? userId = null)
        {
            Visit? visit = new Visit();
            if (userId == null)
            {
                visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id);
            }
            else
            {
                visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id && v.OwnedBy == userId);
            }
            if (visit == null) return (false, null, $"No visit was found with Id {id}");
            VisitDTO visitDTO = _mapper.Map<VisitDTO>(visit);
            return (true, visitDTO, null);
        }

        public async Task<(bool IsSuccess, VisitAggregateDTO? Visit, string? ErrorMessage)> GetVisitAggregateAsync(int id, string? userId = null)
        {
            Visit? visit = new Visit();
            if (userId == null)
            {
                visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id, includeProperties: "PetNavigation,VetNavigation");
            }
            else
            {
                visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id && v.OwnedBy == userId, includeProperties: "PetNavigation,VetNavigation");
            }

            if (visit == null || visit!.Id == 0) return (false, null, $"No visit was found with Id {id}");
            else
            {
                VisitAggregateDTO? visitAggregate = new VisitAggregateDTO()
                {
                    OwnedBy = visit.OwnedBy,
                    Id = visit.Id,
                    PetId = visit.PetId,
                    PetName = visit.PetNavigation!.Name,
                    PetImageURL = visit.PetNavigation.ImageURL,
                    VetId = visit.VetId,
                    Doctor = visit.VetNavigation!.Doctor,
                    Hospital = visit.VetNavigation.Hospital,
                    DateOfVisit = visit.DateOfVisit,
                    VisitType = visit.VisitType,
                    Reason = visit.Reason,
                    Weight = visit.Weight,
                    Diagnosis = visit.Diagnosis,
                    Prescriptions = visit.Prescriptions,
                    Instructions = visit.Instructions,
                    Notes = visit.Notes,
                    Heartworm = visit.Heartworm,
                    Bordatella = visit.Bordatella,
                    Rabies = visit.Rabies,
                    Da2ppv = visit.Da2ppv,
                    Leptospirosis = visit.Leptospirosis,
                    InfluenzaH3N2 = visit.InfluenzaH3N2,
                    InfluenzaH3N8 = visit.InfluenzaH3N8,
                    FecalTest = visit.FecalTest,
                    OtherTests = visit.OtherTests
                };
                return (true, visitAggregate, null);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddVisitAsync(VisitDTO visitDTO)
        {
            Visit visit = _mapper.Map<Visit>(visitDTO);
            await _unitOfWork.Visits.AddAsync(visit);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditVisitAsync(int id, VisitDTO visitDTO)
        {
            if (visitDTO.Id != id) return (false, "Id does not match the object Id");
            Visit? visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id);
            if (visit == null) return (false, $"No visit was found with Id {id}");

            visit.VetId = visitDTO.VetId!;
            visit.PetId = visitDTO.PetId!;
            visit.DateOfVisit = visitDTO.DateOfVisit!;

            visit.VisitType = visitDTO.VisitType!;
            visit.Reason = visitDTO.Reason!;
            visit.Weight = visitDTO.Weight!;
            visit.Diagnosis = visitDTO.Diagnosis!;

            visit.Prescriptions = visitDTO.Prescriptions!;
            visit.Instructions = visitDTO.Instructions!;
            visit.Notes = visitDTO.Notes!;
            visit.Heartworm = visitDTO.Heartworm!;

            visit.Bordatella = visitDTO.Bordatella!;
            visit.Rabies = visitDTO.Rabies!;
            visit.Da2ppv = visitDTO.Da2ppv!;
            visit.Leptospirosis = visitDTO.Leptospirosis!;

            visit.InfluenzaH3N2 = visitDTO.InfluenzaH3N2!;
            visit.InfluenzaH3N8 = visitDTO.InfluenzaH3N8!;
            visit.FecalTest = visitDTO.FecalTest!;
            visit.OtherTests = visitDTO.OtherTests!;

            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteVisitAsync(int id)
        {
            Visit? visit = await _unitOfWork.Visits.GetFirstOrDefaultAsync(filter: v => v.Id == id);
            if (visit == null) return (false, $"No visit was found with Id {id}");

            await _unitOfWork.Visits.RemoveAsync(visit);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }
    }
}
