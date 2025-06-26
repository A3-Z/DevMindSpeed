namespace DevMindSpeed.Common.Db.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginatedResult()
        {
            Items = new List<T>();
        }
        public PaginatedResult(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
        }
    }
}