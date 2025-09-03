using AutoMapper;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.MapperProfiles
{
    public class PetHealthcareConfig : Profile
    {
        public PetHealthcareConfig()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<Vet, VetDTO>().ReverseMap();
            CreateMap<Visit, VisitDTO>().ReverseMap();
            CreateMap<CarouselImage, CarouselImageDTO>().ReverseMap();
        }
    }
}
