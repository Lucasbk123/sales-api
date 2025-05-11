using Ambev.DeveloperEvaluation.Application.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale
{
    public class GetPaginatedSaleCommand : IRequest<PaginatedCommandResult<GetPaginatedSaleCommandResult>>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}