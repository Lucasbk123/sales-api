using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class PatchSaleItemCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDiscountStrategy _discountStrategy;
        private readonly IPublisher _publisher;
        private readonly PatchSaleItemCommandHandler _handler;

        public PatchSaleItemCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _publisher = Substitute.For<IPublisher>();
            _discountStrategy = Substitute.For<IDiscountStrategy>();
            _handler = new PatchSaleItemCommandHandler(_saleRepository, _discountStrategy, _publisher);
        }


        [Fact(DisplayName = @"Given an existing item, when a valid update request is handled,
                              then the handler executes successfully")]
        public async Task Give_ExistingItem_When_UpdateValidRequest_Then_HandleMethodIsExecuted()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var saleItem = sale.Items.First();

            var command = saleItem.ToMapperCommand(sale.Id);

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            await _handler.Handle(command, CancellationToken.None);


            // Assert

            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _publisher.Received(1).Publish(Arg.Any<SaleUpdateEvent>(), Arg.Any<CancellationToken>());
        }


        [Fact(DisplayName = "Validation should fail for PatchSaleItemCommand data")]
        public void Given_InvalidPatchSaleItemCommand_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var command = new PatchSaleItemCommand();

            var validator = new PatchSaleItemCommandValidator();

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
        }


        [Fact(DisplayName = "Given a no existent sale,then a KeyNotFoundException is thrown")]
        public async Task Give_NotExistSale_When_UpdateInValidRequest_Then_ThrowsKeyNotFoundException()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var saleItem = sale.Items.First();

            var command = saleItem.ToMapperCommand(sale.Id);

            _saleRepository.GetByIdAsync(Guid.NewGuid()).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);


            // Assert

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact(DisplayName = "Given a no existent sale Item, then a KeyNotFoundException is thrown")]
        public async Task Give_NotExistSaleItem_When_UpdateInValidRequest_Then_ThrowsKeyNotFoundException()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var saleItem = SaleCommonTestData.GenerateValidSaleItem(sale.Id);

            var command = saleItem.ToMapperCommand(sale.Id);

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);


            // Assert

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact(DisplayName = "Given a cancelled sale, then a ValidationException is thrown")]
        public async Task Give_SaleCancelled_When_UpdateValidRequest_Then_ThrowsValidationException()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var saleItem = sale.Items.First();

            sale.Cancel();

            var command = saleItem.ToMapperCommand(sale.Id);

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);


            // Assert

            await act.Should().ThrowAsync<ValidationException>();
        }

    }
}
