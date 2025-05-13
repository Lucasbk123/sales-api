using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Strategies;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.Common;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IDiscountStrategy _discountStrategy;
    private readonly IPublisher _publisher;
    private readonly CreateSaleCommandHandler _handler;
    private readonly IMapper _mapper;

    public CreateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _publisher = Substitute.For<IPublisher>();
        _discountStrategy = Substitute.For<IDiscountStrategy>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateSaleCommandHandler(_mapper, _discountStrategy, _saleRepository, _publisher);
    }


    [Fact(DisplayName = "Given an  sale, when request is valid create handler executes successfully")]
    public async Task Give_RequestValid_When_Create_Then_HandleMethodIsExecuted()
    {
        // Arrange
        var sale = SaleCommonTestData.GenerateValidSale(3);

        var command = sale.ToMapperCreateSaleCommand();

        var result = new CreateSaleCommandResult { Id = sale.Id };

        _mapper.Map<CreateSaleCommandResult>(Arg.Any<Sale>()).Returns(result);
        // Act
        var createSaleResult = await _handler.Handle(command, CancellationToken.None);

        // Assert
        createSaleResult.Should().NotBeNull();
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
        await _publisher.Received(1).Publish(Arg.Any<SaleCreateEvent>(), Arg.Any<CancellationToken>());
    }


    [Fact(DisplayName = "Validation should fail for CreateSaleCommand data")]
    public void Given_InvalidCreateSaleCommand_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var command = new CreateSaleCommand();

        var validator = new CreateSaleCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }
}
