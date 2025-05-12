using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel
{
    public class PatchSaleItemCancelCommandHandler : IRequestHandler<PatchSaleItemCancelCommand>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public PatchSaleItemCancelCommandHandler(
            ISaleRepository saleRepository,
            IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task Handle(PatchSaleItemCancelCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"sale with ID {command.SaleId} not found");

            if (sale.Items.FirstOrDefault(x => x.ProductId.Equals(command.ProductId)) == null)
                throw new KeyNotFoundException($"product with ID {command.ProductId} not found");

            if (sale.Status == Domain.Enums.SaleStatus.Canceled)
                throw new ValidationException($"sale with canceled status cannot be changed");

            sale.CancelItem(command.ProductId);

            await _publisher.Publish(new SaleItemCancel(sale.Id, command.ProductId));
            await _saleRepository.UpdateAsync(sale, cancellationToken);
        }
    }
}
