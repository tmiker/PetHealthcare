namespace PetHealthcare.MVC.Paging
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public string? urlParam { get; set; }   // stores curent page number in a url
    }
}
