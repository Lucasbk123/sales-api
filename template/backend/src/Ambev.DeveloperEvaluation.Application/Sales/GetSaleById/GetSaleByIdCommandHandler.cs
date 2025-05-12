using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdCommandHandler : IRequestHandler<GetSaleByIdCommand, GetSaleByIdCommandResult>
{
    private readonly IMapper _mapper;
    private readonly ISaleRepository _saleRepository;

    public GetSaleByIdCommandHandler(IMapper mapper, ISaleRepository saleRepository)
    {
        _mapper = mapper;
        _saleRepository = saleRepository;
    }

    public async Task<GetSaleByIdCommandResult> Handle(GetSaleByIdCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id,cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"sale with ID {command.Id} not found");

        return _mapper.Map<GetSaleByIdCommandResult>(sale);
    }
}