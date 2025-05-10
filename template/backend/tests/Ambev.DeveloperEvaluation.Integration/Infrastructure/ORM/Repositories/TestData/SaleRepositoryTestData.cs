using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.Infrastructure.ORM.Repositories.TestData
{
    public class SaleRepositoryTestData
    {

        private readonly static Faker<Sale> SaleFaker = new Faker<Sale>()
             .RuleFor(s => s.BranchId, f => f.Random.Guid())
             .RuleFor(s => s.CustomerId, f => f.Random.Guid())
             .RuleFor(s => s.BranchName, f => f.Address.City())
             .RuleFor(s => s.Number, f => f.Random.Int(1, 100))
             .RuleFor(s => s.CustomerName, f => f.Person.FullName)
             .RuleFor(s => s.CreatedAt, f => f.Date.Past())
             .RuleFor(s => s.Status, SaleStatus.Confirmed)
             .RuleFor(s => s.CreatedAt,f => DateTime.UtcNow);

        private readonly static Faker<SaleItem> SaleItemFakeBase = new Faker<SaleItem>()
                .RuleFor(s => s.ProductId, f => f.Random.Guid())
                .RuleFor(s => s.UnitPrice, f => f.Random.Decimal(50, 400))
                .RuleFor(s => s.Quantity, f => f.Random.Short(1, 20))
                .RuleFor(s => s.Discount, f => f.Random.Decimal(10, 40))
                .RuleFor(s => s.ProductName, f => f.Commerce.ProductName());



        public static Sale GenerateValidSale(int itemsQuantity = 3)
        {
            var sale = SaleFaker.Generate();

            SaleItemFakeBase
             .RuleFor(s => s.SaleId, sale.Id);

            var items = SaleItemFakeBase.Generate(itemsQuantity);

            foreach (var item in items)
                sale.AddItem(item);

            return sale;
        }
    }
}
