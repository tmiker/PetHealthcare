using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.Pet
{
    public class PetUpsertViewModel
    {
        public PetDTO? Pet { get; set; }
        public List<string> GenderList = new List<string>();
    }
}
