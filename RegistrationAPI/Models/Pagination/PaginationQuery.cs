namespace RegistrationAPI.Models.NewFolder
{
    public class PaginationQuery
    {
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public bool? Descending { get; set; }
    }
}
