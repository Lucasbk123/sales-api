using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleItem
{
    public class PatchSaleItemProfile : Profile
    {
        public PatchSaleItemProfile()
        {
            CreateMap<PatchSaleItemRequest, PatchSaleItemCommand>()
                .ForMember(dest => dest.SaleId, source => source.MapFrom((source, dest, destMember, context) => context.Items["SaleId"]))
                .ForMember(dest => dest.ProductId, source => source.MapFrom((source, dest, destMember, context) => context.Items["ProductId"]));
        }
    }
}