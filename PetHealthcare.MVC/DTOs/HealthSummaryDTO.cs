namespace PetHealthcare.MVC.DTOs
{
    public class HealthSummaryDTO
    {
        public int VisitCount { get; set; }
        public int PetId { get; set; }
        public string? PetName { get; set; }
        public string? ImageURL { get; set; }
        public string? NextAnnualExamDueDate { get; set; } = "no data available";
        public string? RabiesDue { get; set; } = "no data available";
        public string? Da2ppvDue { get; set; } = "no data available";
        public string? LeptospirosisDue { get; set; } = "no data available";
        public string? HeartwormDue { get; set; } = "no data available";
        public string? BordatellaDue { get; set; } = "no data available";
        public string? InfluenzaH3N2Due { get; set; } = "no data available";
        public string? InfluenzaH3N8Due { get; set; } = "no data available";
        public string? FecalTestDue { get; set; } = "no data available";

    }
}
