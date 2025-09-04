using AutoMapper;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class VetService : IVetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<VetDTO>? Vets, string? ErrorMessage)> GetAllVetsAsync(string? userId = null)
        {
            IEnumerable<VetDTO> vetDTOs = new List<VetDTO>();
            IEnumerable<Vet> vets = new List<Vet>();
            if (userId == null)
            {
                vets = await _unitOfWork.Vets.GetAllAsync();
            }
            else
            {
                vets = await _unitOfWork.Vets.GetAllAsync(filter: p => p.OwnedBy == userId);
            }
            vetDTOs = _mapper.Map<IEnumerable<VetDTO>>(vets);
            return (true, vetDTOs, null);
        }

        public async Task<(bool IsSuccess, VetDTO? Vet, string? ErrorMessage)> GetVetAsync(int id, string? userId = null)
        {
            Vet? vet = new Vet();
            if (userId == null)
            {
                vet = await _unitOfWork.Vets.GetFirstOrDefaultAsync(filter: v => v.Id == id);
            }
            else
            {
                vet = await _unitOfWork.Vets.GetFirstOrDefaultAsync(filter: v => v.Id == id && v.OwnedBy == userId);
            }
            if (vet == null) return (false, null, $"No vet was found with Id {id}");
            VetDTO vetDTO = _mapper.Map<VetDTO>(vet);
            return (true, vetDTO, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddVetAsync(VetDTO vetDTO)
        {
            Vet vet = _mapper.Map<Vet>(vetDTO);
            await _unitOfWork.Vets.AddAsync(vet);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditVetAsync(int id, VetDTO vetDTO)
        {
            if (vetDTO.Id != id) return (false, "Id does not match the object Id");
            Vet? vet = await _unitOfWork.Vets.GetFirstOrDefaultAsync(filter: v => v.Id == id);
            if (vet == null) return (false, $"No vet was found with Id {id}");

            vet.Hospital = vetDTO.Hospital;
            vet.Doctor = vetDTO.Doctor;
            vet.Phone = vetDTO.Phone;
            vet.Street1 = vetDTO.Street1;
            vet.Street2 = vetDTO.Street2;
            vet.City = vetDTO.City;
            vet.State = vetDTO.State;
            vet.ZipCode = vetDTO.ZipCode;
            vet.ImageBytes = vetDTO.ImageBytes;

            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteVetAsync(int id)
        {
            Vet? vet = await _unitOfWork.Vets.GetFirstOrDefaultAsync(filter: v => v.Id == id, includeProperties: "Visits");
            if (vet == null) return (false, $"No vet was found with Id {id}");
            foreach (var visit in vet.Visits)
            {
                await _unitOfWork.Visits.RemoveAsync(visit);
            }
            await _unitOfWork.Vets!.RemoveAsync(vet);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }
    }
}
