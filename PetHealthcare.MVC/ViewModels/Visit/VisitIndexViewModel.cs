using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;

namespace PetHealthcare.MVC.ViewModels.Visit
{
    public class VisitIndexViewModel
    {
        public List<VisitAggregateDTO> VisitAggregates { get; set; } = new List<VisitAggregateDTO>();
        public PagingInfo? PagingInfo { get; set; }
    }
}
