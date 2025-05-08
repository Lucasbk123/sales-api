using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandProfile : Profile
{
    public CreateSaleCommandProfile()
    {
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateSaleItemCommand, SaleItem>();
        CreateMap<Sale, CreateSaleCommandResult>();
    }
}
