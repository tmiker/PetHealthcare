namespace PetHealthcare.MVC.Utility
{
    public class StaticDetails
    {
        public const string HttpClientName = "Pet_Healthcare_Api_Client";
        public const string ApiBaseUrl = "https://localhost:7068";         // "http s://pethealthapp.azurewebsites.net";   
        public const string PetApiPath = "/api/pets";
        public const string VetApiPath = "/api/vets";
        public const string VisitApiPath = "/api/visits";
        public const string AdminUsersApiPath = "/api/adminUsers";
        public const string CarouselImageApiPath = "/api/carouselImages";

        public static List<string> GenderList = new List<string>() { "Male", "Female" };
        public static List<string> VisitTypelist = new List<string>() { "Annual Exam", "Routine", "Minor Incident", "Emergency Care", "Shots Due", "Shelter Care" };
    }
}
