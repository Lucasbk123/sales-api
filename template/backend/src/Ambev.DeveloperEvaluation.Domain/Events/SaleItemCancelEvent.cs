using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelEvent : IEvent
    {
        public SaleItemCancelEvent(Guid saleId, Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }

        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
    }
}