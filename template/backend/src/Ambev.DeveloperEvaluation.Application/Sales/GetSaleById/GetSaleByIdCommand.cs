using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdCommand  : IRequest<GetSaleByIdCommandResult>
{
    public Guid Id { get; set; }
}
