using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    protected Sale()
    {
        CreateAt = DateTime.UtcNow;
        Status = SaleStatus.Pending;
        Items = [];
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public decimal TotalValue { get; private set; }

    public DateTime CreateAt { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public SaleStatus Status { get; set; }

    public ICollection<SaleItem> Items { get; private set; }


    public static Sale CreateSale(Guid customerId, string customerName, Guid branchId, string branchName)
    {
        return new Sale()
        {
            CustomerId = customerId,
            CustomerName = customerName,
            BranchId = branchId,
            BranchName = branchName,
        };
    }

    public void AddItem(SaleItem saleItem)
    {
        Items.Add(saleItem);

        TotalValue += saleItem.GetTotalValueWithDiscount();
    }

    public void UpdateItem(Guid prodcutId, decimal unitPrice, short quantity, decimal discount)
    {
        var item = Items.First(x => x.ProductId.Equals(prodcutId));

        item.UpdateItem(unitPrice, quantity, discount);

        RecalculateValueTotalItems();
    }

    public void CancelItem(Guid productId)
    {
        Items.First(x => x.ProductId == productId).Cancel();

        RecalculateValueTotalItems();
    }

    private void RecalculateValueTotalItems() => TotalValue = Items
                                                             .Where(x => !x.Cancelled)
                                                             .Sum(x => x.GetTotalValueWithDiscount());

}

public class SaleItem
{
    public SaleItem()
    {

    }
    public SaleItem(Guid saleId, Guid productId, decimal unitPrice, short quantity, decimal discount)
    {
        SaleId = saleId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
    }

    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public decimal Discount { get; set; }

    public bool Cancelled { get; set; }

    public void UpdateItem(decimal price, short quantity, decimal discount)
    {
        Quantity = quantity;
        UnitPrice = price;
        Discount = discount;
    }

    public void Cancel() => Cancelled = true;

    public decimal GetTotalValueWithDiscount() => (UnitPrice * Quantity) - Discount;

    public decimal GetTotalValue() => (UnitPrice * Quantity);


    //Entity Framework
    public Sale Sale { get; set; }
}
