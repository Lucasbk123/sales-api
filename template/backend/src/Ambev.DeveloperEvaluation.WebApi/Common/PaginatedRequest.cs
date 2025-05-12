namespace Ambev.DeveloperEvaluation.WebApi.Common;

public abstract class PaginatedRequest
{
    public int Page { get; set; }

    public int PageSize { get; set; }
}
