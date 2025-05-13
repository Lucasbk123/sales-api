using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class DeleteSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly DeleteSaleCommandHandler _handler;

    public DeleteSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _handler = new DeleteSaleCommandHandler(_saleRepository);
    }


    [Fact(DisplayName = "Given an existing sale, when request is valid ,the handler executes successfully")]
    public async Task Give_ExistingSale_When_ValidRequest_Then_HandleMethodIsExecuted()
    {
        // Arrange
        var sale = SaleCommonTestData.GenerateValidSale(3);

        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };

        sale.Id = command.Id;

        _saleRepository.DeleteAsync(sale.Id).Returns(true);

        // Act
        await _handler.Handle(command, CancellationToken.None);


        // Assert

        await _saleRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }


    [Fact(DisplayName = "Validation should fail for DeleteSaleCommand data")]
    public void Given_InvalidDeleteSaleCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new DeleteSaleCommand();

        var validator = new DeleteSaleCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }


    [Fact(DisplayName = "Given a non existent sale,then a KeyNotFoundException is thrown")]
    public async Task Give_NotExistSale_When_InValidRequest_Then_ThrowsKeyNotFoundException()
    {
        // Arrange
        var sale = SaleCommonTestData.GenerateValidSale(3);

        var command = new DeleteSaleCommand { Id = Guid.NewGuid() };

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);


        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
