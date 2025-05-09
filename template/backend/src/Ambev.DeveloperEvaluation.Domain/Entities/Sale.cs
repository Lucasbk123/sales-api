using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    protected Sale()
    {
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Pending;
        Items = [];
        Id = Guid.NewGuid();
    }

    public long Number { get; set; }

    public decimal TotalValue { get; private set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public SaleStatus Status { get; set; }

    public ICollection<SaleItem> Items { get; private set; }

    public DateTime CreatedAt { get; set; }


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

    public void Update(Guid customerId, string customerName, Guid branchId, string branchName, IEnumerable<SaleItem> saleItems)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
        UpdateAllItem(saleItems);
    }


    private void UpdateAllItem(IEnumerable<SaleItem> saleItems)
    {
        var itemsCancel = Items.Where(x => x.Cancelled).ToList();

        Items.Clear();

        foreach (var item in saleItems.Where(x => !itemsCancel.Contains(x)))
            itemsCancel.Add(item);

        Items = itemsCancel;
        RecalculateValueTotalItems();
    }

    public void AddItem(SaleItem saleItem)
    {
        Items.Add(saleItem);

        TotalValue += saleItem.GetTotalValueWithDiscount();
    }

    public void UpdateSaleItem(Guid prodcutId, decimal unitPrice, short quantity, decimal discount)
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
    public SaleItem(Guid saleId, Guid productId, string productName, decimal unitPrice, short quantity, decimal discount)
    {
        SaleId = saleId;
        ProductId = productId;
        UnitPrice = unitPrice;
        Quantity = quantity;
        Discount = discount;
        ProductName = productName;
    }

    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }

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

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var saleItem = (SaleItem)obj;

        return ProductId == saleItem.ProductId && SaleId == saleItem.SaleId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SaleId, ProductId);
    }


    //Entity Framework
    public Sale Sale { get; set; }
}
