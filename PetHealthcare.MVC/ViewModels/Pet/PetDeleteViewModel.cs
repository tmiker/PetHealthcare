using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.Pet
{
    public class PetDeleteViewModel
    {
        public PetDTO? Pet { get; set; }
        public int VisitCount { get; set; } = 0;
    }
}
