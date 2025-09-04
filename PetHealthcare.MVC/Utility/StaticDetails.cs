namespace PetHealthcare.MVC.Utility
{
    public class StaticDetails
    {
        public const string PetHealthcareApi_ClientName = "Pet_Healthcare_Api_Client";
        public const string PetHealthcareApi_BaseUrl = "https://localhost:7068";         // "http s://pethealthapp.azurewebsites.net";   
        public const string PetHealthcareApi_AuthPath = "/api/auth";
        public const string PetHealthcareApi_AccountPath = "/api/account";
        public const string PetHealthcareApi_PetPath = "/api/pets";
        public const string PetHealthcareApi_VetPath = "/api/vets";
        public const string PetHealthcareApi_VisitPath = "/api/visits";
        public const string PetHealthcareApi_AdminUsersPath = "/api/adminUsers";
        public const string PetHealthcareApi_CarouselImagesPath = "/api/carouselImages";

        public static List<string> GenderList = new List<string>() { "Male", "Female" };
        public static List<string> VisitTypelist = new List<string>() { "Annual Exam", "Routine", "Minor Incident", "Emergency Care", "Shots Due", "Shelter Care" };
    }
}
