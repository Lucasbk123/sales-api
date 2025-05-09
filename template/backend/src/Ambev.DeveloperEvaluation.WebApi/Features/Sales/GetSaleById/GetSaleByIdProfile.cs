using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleByIdProfile : Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();
            CreateMap<GetSaleByIdCommandResult, GetSaleByIdResponse>();
            CreateMap<GetSaleItemByIdCommandResult, GetSaleByIdItemResponse>();
        }
    }
}