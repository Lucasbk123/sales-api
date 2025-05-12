using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleCancel;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItemCancel;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetSaleByIdAsync([FromQuery] GetPaginatedSaleRequest request , CancellationToken cancellationToken)
    {
        var validator = new GetPaginatedSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetPaginatedSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(_mapper.Map<PaginatedResponse<GetPaginatedSaleResponse>>(response));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created($"/api/Sales/{response.Id}", new ApiResponseWithData<CreateSaleResponse>
        {
            Success = true,
            Message = "sale created successfully",
            Data = _mapper.Map<CreateSaleResponse>(response)
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSaleByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetSaleByIdRequest { Id = id };

        var validator = new GetSaleByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetSaleByIdCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetSaleByIdResponse>
        {
            Success = true,
            Message = "Sale retrived successfully",
            Data = _mapper.Map<GetSaleByIdResponse>(response)
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSaleByIdAsync([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request, opts => opts.Items["Id"] = id);


        await _mediator.Send(command, cancellationToken);

        return NoContent();

    }

    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> PatcheSaleByIdCancelAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new PatchSaleCancelRequest { Id = id };

        var validator = new PatchSaleCancelValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<PatchSaleCancelCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/product/{productId:guid}")]
    public async Task<IActionResult> PatcheSaleByIdAsync([FromRoute] Guid id, [FromRoute] Guid productId, [FromBody] PatchSaleItemRequest request, CancellationToken cancellationToken)
    {
        var validator = new PatchSaleItemRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<PatchSaleItemCommand>(request, opts =>
        {

            opts.Items["SaleId"] = id;
            opts.Items["ProductId"] = productId;
        });

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPatch("{id:guid}/product/{productId:guid}/cancel")]
    public async Task<IActionResult> PatcheSaleItemCancelAsync([FromRoute] Guid id, [FromRoute] Guid productId, CancellationToken cancellationToken)
    {

        var request = new PatchSaleItemCancelRequest { ProductId = productId, SaleId = id };

        var validator = new PatchSaleItemCancelRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<PatchSaleItemCancelCommand>(request);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSaleByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteSaleRequest { Id = id };

        var validator = new DeleteSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteSaleCommand>(request);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

}
