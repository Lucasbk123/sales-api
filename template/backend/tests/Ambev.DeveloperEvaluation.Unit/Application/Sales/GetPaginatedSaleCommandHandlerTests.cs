using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using AutoMapper;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class GetPaginatedSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetPaginatedSaleCommandHandler _handler;

    public GetPaginatedSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetPaginatedSaleCommandHandler(_saleRepository, _mapper);
    }


    [Fact(DisplayName = "Given an existing sale, when request is valid ,returns sale by filter with success")]
    public async Task Give_Sales_When_GetValidRequest_Then_ReturnsSaleByFilterWithSuccess()
    {
        // Arrange

        var sales = SaleCommonTestData.GenerateValidSales(11);

        var command = new GetPaginatedSaleCommand { Page = 1, PageSize = 10 };

        var result = sales.ToMappePaginatedCommandResult(command.Page, command.PageSize, 11);

        _mapper.Map<PaginatedCommandResult<GetPaginatedSaleCommandResult>>(sales).Returns(result);

        _saleRepository.GetByPaginedFilterAsync(command.Page, command.PageSize).Returns((sales, sales.Count()));

        // Act
        var getPaginatedCommandResult = await _handler.Handle(command, CancellationToken.None);


        // Assert
        getPaginatedCommandResult.Data.IsNullOrEmpty();
        getPaginatedCommandResult.TotalPages.Should().Be(2);
    }


    [Fact(DisplayName = "Validation should fail for GetPaginatedSaleCommand data")]
    public void Given_InvalidGetPaginatedSaleCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new GetPaginatedSaleCommand();

        var validator = new GetPaginatedSaleCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }


}
