using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdCommandProfile : Profile
    {
        public GetSaleByIdCommandProfile()
        {
            CreateMap<Sale, GetSaleByIdCommandResult>();

            CreateMap<SaleItem, GetSaleItemByIdCommandResult>()
                .ForMember(des => des.TotalPrice, source => source.MapFrom(x => x.GetTotalValue()));

        }
    }
}
