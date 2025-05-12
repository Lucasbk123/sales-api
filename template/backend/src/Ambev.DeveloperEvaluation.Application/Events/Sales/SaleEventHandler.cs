using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Events.Sales
{
    public class SaleEventHandler
        : INotificationHandler<SaleCreateEvent>,
          INotificationHandler<SaleUpdateEvent>,
          INotificationHandler<SaleItemCancel>,
          INotificationHandler<SaleCancelEvent>
    {

        private readonly ILogger<SaleEventHandler> _logger;

        public SaleEventHandler(ILogger<SaleEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(SaleCancelEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"sale cancelled with id:{notification.SaleId}");
            await Task.CompletedTask;
        }

        public async Task Handle(SaleItemCancel notification, CancellationToken cancellationToken)
        {
             _logger.LogInformation($"Item id:{notification.ProductId} has been cancelled from sale id:{notification.SaleId}");
            await Task.CompletedTask;
        }

        public async Task Handle(SaleUpdateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"sale modified with id:{notification.SaleId}");
            await Task.CompletedTask;
        }

        public async Task Handle(SaleCreateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"sale created with id:{notification.SaleId}");
            await Task.CompletedTask;
        }
    }
}