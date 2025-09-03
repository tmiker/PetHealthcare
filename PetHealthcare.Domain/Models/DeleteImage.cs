namespace PetHealthcare.Domain.Models
{
    public class DeleteImage : BaseEntity
    {
        public string? ImageURL { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
