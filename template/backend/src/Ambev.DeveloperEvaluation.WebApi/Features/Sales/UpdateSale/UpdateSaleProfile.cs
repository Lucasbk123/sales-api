﻿using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
            .ForMember(dest => dest.Id,source => source.MapFrom((source, dest, destMember, context) =>
                context.Items["Id"]));
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();
    }
}