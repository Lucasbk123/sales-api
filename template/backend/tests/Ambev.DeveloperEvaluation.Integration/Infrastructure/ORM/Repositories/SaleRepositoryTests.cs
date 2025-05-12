using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Integration.Infrastructure.ORM.Repositories.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Infrastructure.ORM.Repositories
{

    public sealed partial class SaleRepositoryTests : IClassFixture<PostgreSqlDefaultFixture>
    {
        private readonly ISaleRepository _saleRepository;

        public SaleRepositoryTests(PostgreSqlDefaultFixture fixture)
        {

            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseNpgsql(fixture.ConnectionString)
                .Options;

            var defaultContext = new DefaultContext(options);
            defaultContext.Database.EnsureCreated();

            _saleRepository = new SaleRepository(defaultContext);
        }

        [Xunit.Fact(DisplayName = "Given valid sale when added then should be in repository")]
        public async Task Given_ValidSale_When_Added_Then_ShouldBeInRepository()
        {
            // Arrange
            var sale = Sale.CreateSale(Guid.NewGuid(), "Lucas Pereira Alves", Guid.NewGuid(), "Araguaina - TO");

            sale.AddItem(new SaleItem(sale.Id, Guid.NewGuid(), "Cerveja", 10, 5, 10));

            //// Act
            await _saleRepository.CreateAsync(sale);
            var saleNew = await _saleRepository.GetByIdAsync(sale.Id);

            // Assert
            Assert.NotNull(saleNew);
            Assert.Equal("Lucas Pereira Alves", saleNew.CustomerName);
        }

        [Fact(DisplayName = "Given the existing sale update with the new data")]
        public async Task Given_ExistingSale_When_Updated_Then_ShouldReflectChanges()
        {
            // Arrange
            var saleOld = SaleRepositoryTestData.GenerateValidSale();
            var saleUpdate = SaleRepositoryTestData.GenerateValidSale(6);
            await _saleRepository.CreateAsync(saleOld);

            //// Act

            var saleNew = await _saleRepository.GetByIdAsync(saleOld.Id);

            saleNew?.Update(saleUpdate.CustomerId, saleUpdate.CustomerName,
                saleUpdate.BranchId, saleUpdate.BranchName, saleUpdate.Items);

            await _saleRepository.UpdateAsync(saleNew);

            var saleNewUpdate =  await _saleRepository.GetByIdAsync(saleNew.Id);


            // Assert
            Assert.Equal(saleUpdate.TotalValue,saleNewUpdate.TotalValue);
            Assert.Equal(saleUpdate.Items.Count, saleNew.Items.Count);
        }

        [Fact(DisplayName = "Given the existing sale, delete the sale from the database")]
        public async Task Given_ExistingSale_When_Delete_Then_ShouldRemoveSale()
        {
            // Arrange
            var saleOld = SaleRepositoryTestData.GenerateValidSale();
            await _saleRepository.CreateAsync(saleOld);

            //// Act

            var saleNew = await _saleRepository.DeleteAsync(saleOld.Id);


            // Assert
            Assert.True(saleNew);
        }
    }
}