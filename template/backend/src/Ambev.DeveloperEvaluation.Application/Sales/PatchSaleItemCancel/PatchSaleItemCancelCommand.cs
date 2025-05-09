using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;

public class PatchSaleItemCancelCommand : IRequest
{
    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }
}