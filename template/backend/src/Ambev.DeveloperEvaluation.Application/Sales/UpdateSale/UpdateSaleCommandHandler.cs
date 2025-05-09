using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategys;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDiscountStrategy _discountStrategy;

    public UpdateSaleCommandHandler(ISaleRepository saleRepository, IDiscountStrategy discountStrategy)
    {
        _saleRepository = saleRepository;
        _discountStrategy = discountStrategy;
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

        await _saleRepository.UpdateAsync(sale,cancellationToken);

    }
}
