using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.ViewModels.Pet
{
    public class PetDetailViewModel
    {
        public PetDTO? Pet { get; set; }
        // public List<VisitAggregateDTO> PetVisitAggregates { get; set; } = new List<VisitAggregateDTO>();
    }
}
