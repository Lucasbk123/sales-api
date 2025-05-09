using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchSaleCancel
{
    public class PatchSaleCancelCommand  : IRequest
    {
        public Guid Id { get; set; }
    }
}
