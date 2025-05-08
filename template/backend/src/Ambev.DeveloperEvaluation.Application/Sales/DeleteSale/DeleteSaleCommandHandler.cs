using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            var sucess = await _saleRepository.DeleteAsync(command.Id, cancellationToken);

            if (!sucess)
                throw new KeyNotFoundException($"sale with ID {command.Id} not found");
        }
    }
}