namespace RegistrationAPI.Models.NewFolder
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string OrderBy { get; set; }
        public bool? Descending { get; set; }
    }
}
