using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetSaleByIdCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleByIdCommandHandler _handler;

    public GetSaleByIdCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleByIdCommandHandler(_mapper, _saleRepository);
    }


    [Fact(DisplayName = "Given an existing sale, when request is valid ,returns sale by id with success")]
    public async Task Give_ExistingSale_When_GetValidRequest_Then_ReturnsSaleByIdWithSuccess()
    {
        // Arrange

        var sale = SaleCommonTestData.GenerateValidSale(4);

        var command = new GetSaleByIdCommand { Id = sale.Id };

        var result = sale.ToMapperetSaleItemByIdCommandResult();

        _mapper.Map<GetSaleByIdCommandResult>(sale).Returns(result);

        _saleRepository.GetByIdAsync(command.Id).Returns(sale);

        // Act
        var getSaleById = await _handler.Handle(command, CancellationToken.None);


        // Assert
        getSaleById.Should().NotBeNull();
        getSaleById.Id.Should().Be(sale.Id);
        getSaleById.Items.Count().Should().Be(sale.Items.Count);
    }


    [Fact(DisplayName = "Validation should fail for GetSaleByIdCommand data")]
    public void Given_InvalidGetSaleByIdCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new GetSaleByIdCommand();

        var validator = new GetSaleByIdCommandValidator();

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

        var command = new GetSaleByIdCommand { Id = Guid.NewGuid() };

        _saleRepository.GetByIdAsync(sale.Id).Returns(sale);

        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);


        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
