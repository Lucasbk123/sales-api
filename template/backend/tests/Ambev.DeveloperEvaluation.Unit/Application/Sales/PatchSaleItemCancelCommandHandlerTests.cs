using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class PatchSaleItemCancelCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;
        private readonly PatchSaleItemCancelCommandHandler _handler;

        public PatchSaleItemCancelCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _publisher = Substitute.For<IPublisher>();
            _handler = new PatchSaleItemCancelCommandHandler(_saleRepository, _publisher);
        }


        [Fact(DisplayName = @"Given an existing sale item, when request is valid ,the handler executes successfully")]
        public async Task Give_ExistingItem_When_UpdateValidRequest_Then_HandleMethodIsExecuted()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var command = sale.ToMapperPatchSaleItemCancelCommand();

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            await _handler.Handle(command, CancellationToken.None);


            // Assert

            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _publisher.Received(1).Publish(Arg.Any<SaleItemCancelEvent>(), Arg.Any<CancellationToken>());
        }


        [Fact(DisplayName = "Validation should fail for PatchSaleItemCancelCommand data")]
        public void Given_InvalidPatchSaleItemCommand_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var command = new PatchSaleItemCancelCommand();

            var validator = new PatchSaleItemCancelCommandValidator();

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

            var command = sale.ToMapperPatchSaleItemCancelCommand();

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

            var command = sale.ToMapperPatchSaleItemCancelCommand(Guid.NewGuid());

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

            var command = sale.ToMapperPatchSaleItemCancelCommand();

            sale.Cancel();

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);


            // Assert

            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
