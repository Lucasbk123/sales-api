namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItem
{
    public class PatchSaleItemRequest
    {
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }
    }
}