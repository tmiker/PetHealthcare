using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.Vet
{
    public class VetDeleteViewModel
    {
        public VetDTO? Vet { get; set; }
        public int VisitCount { get; set; } = 0;
    }
}
