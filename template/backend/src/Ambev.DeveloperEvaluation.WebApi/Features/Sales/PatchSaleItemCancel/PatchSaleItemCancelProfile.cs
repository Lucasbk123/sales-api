using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItemCancel;

public class PatchSaleItemCancelProfile : Profile
{
    public PatchSaleItemCancelProfile()
    {
        CreateMap<PatchSaleItemCancelRequest,PatchSaleItemCancelCommand>();
    }
}
