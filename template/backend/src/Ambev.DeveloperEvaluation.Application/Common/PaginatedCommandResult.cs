namespace Ambev.DeveloperEvaluation.Application.Common
{
    public class PaginatedCommandResult<T>
    {
        public PaginatedCommandResult(IEnumerable<T> data, int pageNumber, int pageSize, long totalCount)
        {
            Data = data;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public IEnumerable<T> Data { get; set; }

        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public long TotalCount { get; set; }

        public int TotalPages 
            => (int)Math.Ceiling((double)TotalCount / PageSize);

    }
}
