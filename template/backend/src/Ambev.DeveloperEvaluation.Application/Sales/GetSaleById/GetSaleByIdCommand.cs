using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdCommand  : IRequest<GetSaleByIdCommandResult>
{
    public Guid Id { get; set; }
}
