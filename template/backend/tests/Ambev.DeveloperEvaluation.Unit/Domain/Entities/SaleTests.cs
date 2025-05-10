using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {

        [Fact(DisplayName = "Validation should pass for valid sale data")]
        public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }


        [Fact(DisplayName = "Validation should fail for invalid sale data")]
        public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
        {
            // Arrange
            var sale = SaleTestData.GenerateInValidSale();


            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact(DisplayName = "The item should be added, and the total value should be recalculated")]
        public void Given_ValidSale_When_AddItemToSale_Then_ShouldIncreaseItemAndChangeTotalValue()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            var saleItemQuantity = sale.Items.Count + 1;

            var saleItem = SaleTestData.GenerateSaleItems(1).First();

            var saleTotalValue = sale.TotalValue + saleItem.GetTotalValueWithDiscount();

            // Act
            sale.AddItem(saleItem);

            // Assert
            Assert.Equal(sale.Items.Count, saleItemQuantity);
            Assert.Equal(sale.TotalValue, saleTotalValue);
        }


        [Fact(DisplayName = "The item should be update, and the total value should be recalculated")]
        public void Given_ValidSale_When_UpdateItemToSale_Then_ShouldItemAndChangeTotalValue()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            var saleItem = SaleTestData.GenerateSaleItems(1).First();

            var saleItemUpdate = sale.Items.First();

            var itemValue = saleItemUpdate.GetTotalValueWithDiscount() - saleItem.GetTotalValueWithDiscount();

            var saleTotalValue = itemValue >= 0 ?
                sale.TotalValue - itemValue :
                sale.TotalValue + Math.Abs(itemValue);


            // Act
            sale.UpdateSaleItem(saleItemUpdate.ProductId, saleItem.UnitPrice, saleItem.Quantity, saleItem.Discount);

            // Assert
            Assert.Equal(saleTotalValue, sale.TotalValue);
            Assert.Equal(saleItemUpdate.UnitPrice, saleItem.UnitPrice);
            Assert.Equal(saleItemUpdate.Quantity, saleItem.Quantity);
            Assert.Equal(saleItemUpdate.Discount, saleItem.Discount);
        }


        [Fact(DisplayName = "Sale status should change to cancel when canceled")]
        public void Given_ConfirmadSale_When_Canceled_ThenStatusShouldBeCanceled()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.Cancel();

            // Assert
            Assert.Equal(SaleStatus.Canceled, sale.Status);
        }

        [Fact(DisplayName = "Given sale item when canceled then should  recalculate total da sale")]
        public void Given_The_SaleItem_When_Cancelled_TheSaleTotalMustBeRecalculated()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            var saleItem = sale.Items.First();

            var valueTotal = sale.TotalValue - saleItem.GetTotalValueWithDiscount();

            // Act
            sale.CancelItem(saleItem.ProductId);

            // Assert
            Assert.Equal(valueTotal, sale.TotalValue);
            Assert.True(saleItem.Cancelled);
        }

        [Fact(DisplayName = "The sale should be update, and the total value should be recalculated the items")]
        public void Given_ValidSale_When_UpdateSale_Then_ShouldItemsAndChangeTotalValue()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            var saleNew = SaleTestData.GenerateValidSale(5);

            // Act
            sale.Update(saleNew.CustomerId, saleNew.CustomerName,
                saleNew.BranchId, saleNew.BranchName, saleNew.Items);

            // Assert
            Assert.Equal(saleNew.Items.Count, sale.Items.Count);
            Assert.Equal(saleNew.BranchName, sale.BranchName);
            Assert.Equal(saleNew.TotalValue, sale.TotalValue);
        }
    }
}
