using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreateEvent : IEvent
    {
        public SaleCreateEvent(Guid saleId)
        {
            SaleId = saleId;
        }

        public Guid SaleId { get; set; }
    }
}
