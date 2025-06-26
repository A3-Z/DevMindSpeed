namespace DevMindSpeed.Common.Db.Models
{
    public class PaginationParams
    {
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
}