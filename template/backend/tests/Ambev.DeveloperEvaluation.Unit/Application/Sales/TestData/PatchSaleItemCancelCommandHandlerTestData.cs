using Ambev.DeveloperEvaluation.Application.Sales.PatchSaleItemCancel;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

public static class PatchSaleItemCancelCommandHandlerTestData
{
    public static PatchSaleItemCancelCommand ToMapperPatchSaleItemCancelCommand(this Sale sale) =>
    new()
    {
        ProductId = sale.Items.First().ProductId,
        SaleId = sale.Id,
    };

    public static PatchSaleItemCancelCommand ToMapperPatchSaleItemCancelCommand(this Sale sale,Guid productId) =>
    new()
    {
        ProductId = productId,
        SaleId = sale.Id,
    };
}
