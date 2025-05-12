using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;

public class PatchSaleItemCommandHandler : IRequestHandler<PatchSaleItemCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDiscountStrategy _discountStrategy;
    private readonly IPublisher _publisher;

    public PatchSaleItemCommandHandler(
        ISaleRepository saleRepository,
        IDiscountStrategy discountStrategy,
        IPublisher publisher)
    {
        _saleRepository = saleRepository;
        _discountStrategy = discountStrategy;
        _publisher = publisher;
    }

    public async Task Handle(PatchSaleItemCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"sale with ID {command.SaleId} not found");

        if (sale.Items.FirstOrDefault(x => x.ProductId.Equals(command.ProductId)) == null)
            throw new KeyNotFoundException($"product with ID {command.ProductId} not found");

        if (sale.Status == Domain.Enums.SaleStatus.Canceled)
            throw new ValidationException($"sale with canceled status cannot be changed");

        sale.UpdateSaleItem(command.ProductId, command.UnitPrice, command.Quantity,
            _discountStrategy.Calculate(new Product(command.UnitPrice, command.Quantity)));

        await _publisher.Publish(new SaleUpdateEvent(sale.Id));
        await _saleRepository.UpdateAsync(sale, cancellationToken);
    }
}