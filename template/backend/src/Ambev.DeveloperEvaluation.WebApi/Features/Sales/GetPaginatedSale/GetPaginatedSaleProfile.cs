using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale;

public class GetPaginatedSaleProfile : Profile
{
    public GetPaginatedSaleProfile()
    {
        CreateMap<GetPaginatedSaleRequest, GetPaginatedSaleCommand>();
        CreateMap<GetPaginatedSaleCommandResult, GetPaginatedSaleResponse>();
        CreateMap<PaginatedCommandResult<GetPaginatedSaleCommandResult>, PaginatedResponse<GetPaginatedSaleResponse>>()
             .ForMember(dest => dest.Success, opt => opt.MapFrom(_ => true));
    }
}