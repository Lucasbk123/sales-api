namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItemCancel
{
    public class PatchSaleItemCancelRequest
    {
        public Guid SaleId { get; set; }

        public Guid ProductId { get; set; }
    }
}