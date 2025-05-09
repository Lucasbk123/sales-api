using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel
{
    public class PatchSaleItemCancelCommandHandler : IRequestHandler<PatchSaleItemCancelCommand>
    {
        private readonly ISaleRepository _saleRepository;

        public PatchSaleItemCancelCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task Handle(PatchSaleItemCancelCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"sale with ID {command.SaleId} not found");

            if (sale.Items.FirstOrDefault(x => x.ProductId.Equals(command.ProductId)) == null)
                throw new KeyNotFoundException($"product with ID {command.ProductId} not found");

            sale.CancelItem(command.ProductId);

            await _saleRepository.UpdateAsync(sale, cancellationToken);
        }
    }
}
