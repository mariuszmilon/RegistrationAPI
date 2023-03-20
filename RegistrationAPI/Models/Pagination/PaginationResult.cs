namespace RegistrationAPI.Models.NewFolder
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalRows { get; set; }

        public PaginationResult(List<T> data, int totalRows, int pageSize, int pageNumber)
        {
            Data = data;
            TotalRows = totalRows;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemTo = ItemFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalRows / (double)pageSize);
        }
    }
}
