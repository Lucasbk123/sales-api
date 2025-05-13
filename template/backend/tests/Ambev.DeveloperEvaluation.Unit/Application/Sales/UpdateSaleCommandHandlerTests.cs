using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class UpdateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDiscountStrategy _discountStrategy;
    private readonly IPublisher _publisher;
    private readonly UpdateSaleCommandHandler _handler;

    public UpdateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _publisher = Substitute.For<IPublisher>();
        _discountStrategy = Substitute.For<IDiscountStrategy>();
        _handler = new UpdateSaleCommandHandler(_saleRepository, _discountStrategy, _publisher);
    }


    [Fact(DisplayName ="Given an existing sale, when request is valid update,the handler executes successfully")]
    public async Task Give_ExistingSale_When_UpdateValidRequest_Then_HandleMethodIsExecuted()
    {
        // Arrange
        var sale = SaleCommonTestData.GenerateValidSale(3);

        var command = sale.ToMapperCommand();


        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _publisher.Received(1).Publish(Arg.Any<SaleUpdateEvent>(), Arg.Any<CancellationToken>());
    }


    [Fact(DisplayName = "Validation should fail for UpdateSaleCommand data")]
    public void Given_InvalidPatchSaleItemCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new UpdateSaleCommand();

        var validator = new UpdateSaleCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }


    [Fact(DisplayName = "Given a non existent sale,then a KeyNotFoundException is thrown")]
    public async Task Give_NotExistSale_When_UpdateInValidRequest_Then_ThrowsKeyNotFoundException()
    {
        // Arrange
        var sale = SaleCommonTestData.GenerateValidSale(3);

        var command = sale.ToMapperCommand();

        _saleRepository.GetByIdAsync(Guid.NewGuid()).Returns(sale);

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

        var command = sale.ToMapperCommand();

        sale.Cancel();

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}
