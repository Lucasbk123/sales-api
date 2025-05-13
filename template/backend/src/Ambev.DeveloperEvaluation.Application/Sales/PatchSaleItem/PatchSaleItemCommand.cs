using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem
{
    public class PatchSaleItemCommand : IRequest
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
    }
}