using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel;

public class PatchSaleCancelCommandHandler : IRequestHandler<PatchSaleCancelCommand>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;

    public PatchSaleCancelCommandHandler(
        ISaleRepository saleRepository, 
        IPublisher publisher)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
    }

    public async Task Handle(PatchSaleCancelCommand command, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

        if (sale == null)
            throw new KeyNotFoundException($"sale with ID {command.Id} not found");

        if (sale.Status == Domain.Enums.SaleStatus.Canceled)
            throw new ValidationException("sale has already been cancelled");

        sale.Cancel();

        await _publisher.Publish(new SaleCancelEvent(sale.Id));
        await _saleRepository.UpdateAsync(sale, cancellationToken);
    }
}