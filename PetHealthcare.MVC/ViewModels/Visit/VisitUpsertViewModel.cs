using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.Visit
{
    public class VisitUpsertViewModel
    {
        public VisitDTO? Visit { get; set; }
        public List<PetDTO> PetList { get; set; } = new List<PetDTO>();
        public List<VetDTO> VetList { get; set; } = new List<VetDTO>();
        public List<string> VisitTypeList { get; set; } = new List<string>();
    }
}
