using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelEvent : IEvent
    {
        public SaleCancelEvent(Guid saleId)
        {
            SaleId = saleId;
        }

        public Guid SaleId { get; set; }
    }
}