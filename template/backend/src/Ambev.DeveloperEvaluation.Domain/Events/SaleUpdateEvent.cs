using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleUpdateEvent : IEvent
    {
        public SaleUpdateEvent(Guid saleId)
        {
            SaleId = saleId;
        }

        public Guid SaleId { get; set; }
    }
}
