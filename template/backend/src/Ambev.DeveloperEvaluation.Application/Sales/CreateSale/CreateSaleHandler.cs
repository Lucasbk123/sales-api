using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategys;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountStrategy _discountStrategy;
        private readonly ISaleRepository _saleRepository;

        public CreateSaleHandler(IMapper mapper, IDiscountStrategy discountStrategy, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _discountStrategy = discountStrategy;
            _saleRepository = saleRepository;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = Sale.CreateSale(request.CustomerId, request.CustomerName, request.BranchId, request.BranchName);

            foreach (var product in request.Items)
            {

                var discount = _discountStrategy.Calculate(new Product(product.UnitPrice, product.Quantity));

                sale.AddItem(new SaleItem(sale.Id, product.ProductId, product.UnitPrice, product.Quantity, discount));
            }


            await _saleRepository.CreateAsync(sale);

            var teste = _mapper.Map<Sale>(request);

            throw new NotImplementedException();
        }
    }
}
