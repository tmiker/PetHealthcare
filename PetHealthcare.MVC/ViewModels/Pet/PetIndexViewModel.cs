using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;

namespace PetHealthcare.MVC.ViewModels.Pet
{
    public class PetIndexViewModel
    {
        public List<PetDTO> Pets { get; set; } = new List<PetDTO>();
        public PagingInfo? PagingInfo { get; set; }
    }
}
