﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly IDiscountStrategy _discountStrategy;
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public CreateSaleCommandHandler(IMapper mapper,
                                        IDiscountStrategy discountStrategy,
                                        ISaleRepository saleRepository,
                                        IPublisher publisher)
        {
            _mapper = mapper;
            _discountStrategy = discountStrategy;
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<CreateSaleCommandResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = Sale.CreateSale(command.CustomerId, command.CustomerName, command.BranchId, command.BranchName);

            foreach (var product in command.Items)
            {
                var discount = _discountStrategy.Calculate(new Product(product.UnitPrice, product.Quantity));

                sale.AddItem(new SaleItem(sale.Id, product.ProductId, product.ProductName, product.UnitPrice, product.Quantity, discount));
            }

            sale.AuthorizeSale(true);

            await _saleRepository.CreateAsync(sale, cancellationToken);

            await _publisher.Publish(new SaleCreateEvent(sale.Id));
            return _mapper.Map<CreateSaleCommandResult>(sale);
        }
    }
}
