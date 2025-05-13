using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDiscountStrategy _discountStrategy;
    private readonly IPublisher _publisher;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository, 
                                    IDiscountStrategy discountStrategy,
                                    IPublisher publisher)
    {
        _saleRepository = saleRepository;
        _discountStrategy = discountStrategy;
        _publisher = publisher;
    }

    public async Task Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id,cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"sale with ID {command.Id} not found");

        if (sale.Status == Domain.Enums.SaleStatus.Canceled)
            throw new ValidationException($"sale with canceled status cannot be changed");

        var saleitems = command.Items.Select(product =>
         new SaleItem(sale.Id, product.ProductId, product.ProductName, product.UnitPrice, product.Quantity,
         _discountStrategy.Calculate(new Product(product.UnitPrice, product.Quantity))));

        sale.Update(command.CustomerId, command.CustomerName, command.BranchId, command.BranchName, saleitems);

        await _publisher.Publish(new SaleUpdateEvent(sale.Id));
        await _saleRepository.UpdateAsync(sale,cancellationToken);

    }
}
