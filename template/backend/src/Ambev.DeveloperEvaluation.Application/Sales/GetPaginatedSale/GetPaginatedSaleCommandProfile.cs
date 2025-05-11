using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSale;

public class GetPaginatedSaleCommandProfile : Profile
{
    public GetPaginatedSaleCommandProfile()
    {
        CreateMap<Sale, GetPaginatedSaleCommandResult>();
    }
}
