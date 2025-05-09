using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchSaleCancel
{
    public class PatchSaleCancelProfile : Profile
    {
        public PatchSaleCancelProfile()
        {
            CreateMap<PatchSaleCancelRequest, PatchSaleCancelCommand>();
        }
    }
}
