using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;

public class GetPaginatedSaleCommandHandler : IRequestHandler<GetPaginatedSaleCommand, PaginatedCommandResult<GetPaginatedSaleCommandResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetPaginatedSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedCommandResult<GetPaginatedSaleCommandResult>> Handle(GetPaginatedSaleCommand request, CancellationToken cancellationToken)
    {
        var result = await _saleRepository.GetByPaginedFilterAsync(request.Page, request.PageSize);

        return new PaginatedCommandResult<GetPaginatedSaleCommandResult>
            (_mapper.Map<IEnumerable<GetPaginatedSaleCommandResult>>(result.Sales),
            request.Page, request.PageSize, result.TotalRows);
    }
}
