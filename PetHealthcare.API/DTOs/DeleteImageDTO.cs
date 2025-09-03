namespace PetHealthcare.API.DTOs
{
    public class DeleteImageDTO
    {
        public int Id { get; set; }
        public string? OwnedBy { get; set; }
        public string? ImageURL { get; set; }
        public bool IsDeleted { get; set; }

    }
}
