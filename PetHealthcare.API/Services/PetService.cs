using AutoMapper;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class PetService : IPetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<PetDTO>? Pets, string? ErrorMessage)> GetAllPetsAsync(string? userId = null)
        {
            IEnumerable<PetDTO> petDTOs = new List<PetDTO>();
            IEnumerable<Pet> pets = new List<Pet>();
            if (userId == null)
            {
                pets = await _unitOfWork.Pets.GetAllAsync();
            }
            else
            {
                pets = await _unitOfWork.Pets.GetAllAsync(filter: p => p.OwnedBy == userId);
            }
            petDTOs = _mapper.Map<IEnumerable<PetDTO>>(pets);
            return (true, petDTOs, null);
        }

        public async Task<(bool IsSuccess, PetDTO? Pet, string? ErrorMessage)> GetPetAsync(int id, string? userId = null)
        {
            Pet? pet = new Pet();
            if (userId == null)
            {
                pet = await _unitOfWork.Pets.GetFirstOrDefaultAsync(filter: p => p.Id == id);
            }
            else
            {
                pet = await _unitOfWork.Pets.GetFirstOrDefaultAsync(filter: p => p.Id == id && p.OwnedBy == userId);
            }
            if (pet == null) return (false, null, $"No pet was found with Id {id}");
            PetDTO petDTO = _mapper.Map<PetDTO>(pet);
            return (true, petDTO, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddPetAsync(PetDTO petDTO)
        {
            Pet pet = _mapper.Map<Pet>(petDTO);
            await _unitOfWork.Pets.AddAsync(pet);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditPetAsync(int id, PetDTO petDTO)
        {
            if (petDTO.Id != id) return (false, "Id does not match the object Id");
            Pet? pet = await _unitOfWork.Pets.GetFirstOrDefaultAsync(filter: p => p.Id == id);
            if (pet == null) return (false, $"No pet was found with Id {id}");

            pet.OwnedBy = petDTO.OwnedBy;
            pet.Name = petDTO.Name;
            pet.Nickname = petDTO.Nickname;
            pet.Gender = petDTO.Gender;
            pet.Breed = petDTO.Breed;
            pet.DateOfBirth = petDTO.DateOfBirth;
            pet.DateOfAdoption = petDTO.DateOfAdoption;
            pet.ChipNumber = petDTO.ChipNumber;
            pet.Allergies = petDTO.Allergies;
            pet.ImageFileName = petDTO.ImageFileName;
            // handle no image edit, don't null out original
            if (petDTO.ImageURL != null) pet.ImageURL = petDTO.ImageURL;
            else pet.ImageURL = pet.ImageURL;

            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeletePetAsync(int id)
        {
            Pet? pet = await _unitOfWork.Pets.GetFirstOrDefaultAsync(filter: p => p.Id == id, includeProperties: "Visits");
            if (pet == null) return (false, $"No pet was found with an Id of {id}.");
            foreach (var visit in pet.Visits)
            {
                await _unitOfWork.Visits.RemoveAsync(visit);
            }
            await _unitOfWork.Pets!.RemoveAsync(pet);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }
    }
}
