using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;

namespace PetHealthcare.MVC.ViewModels.Vet
{
    public class VetIndexViewModel
    {
        public List<VetDTO> Vets { get; set; } = new List<VetDTO>();
        public PagingInfo? PagingInfo { get; set; }
    }
}
