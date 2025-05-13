using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class PatchSaleCancelCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;
        private readonly PatchSaleCancelCommandHandler _handler;

        public PatchSaleCancelCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _publisher = Substitute.For<IPublisher>();
            _handler = new PatchSaleCancelCommandHandler(_saleRepository, _publisher);
        }


        [Fact(DisplayName = @"Given an existing sale , when request is valid ,the handler executes successfully")]
        public async Task Give_ExistingItem_When_Cancel_Then_HandleMethodIsExecuted()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var command = new PatchSaleCancelCommand { Id = sale.Id };

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _publisher.Received(1).Publish(Arg.Any<SaleCancelEvent>(), Arg.Any<CancellationToken>());
        }


        [Fact(DisplayName = "Validation should fail for PatchSaleCancelCommand data")]
        public void Given_InvalidPatchSaleCancelCommand_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var command = new PatchSaleCancelCommand();

            var validator = new PatchSaleCancelCommandValidator();

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

            var command = new PatchSaleCancelCommand { Id = sale.Id };

            _saleRepository.GetByIdAsync(Guid.NewGuid()).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);


            // Assert

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }


        [Fact(DisplayName = "Given a cancelled sale, then a ValidationException is thrown")]
        public async Task Give_SaleCancelled_When_CancelValidRequest_Then_ThrowsValidationException()
        {
            // Arrange
            var sale = SaleCommonTestData.GenerateValidSale(3);

            var command = new PatchSaleCancelCommand { Id = sale.Id };

            sale.Cancel();

            _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
