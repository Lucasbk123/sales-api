using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(s => s.BranchId)
                .IsRequired();

            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.BranchName)
                .HasMaxLength(50);

            builder.Property(s => s.TotalValue)
                .HasPrecision(18, 2);

            builder.Property(s => s.CustomerName)
                .HasMaxLength(100);

            builder.Property(s => s.Number)
                .ValueGeneratedOnAdd();

            builder.HasMany(s => s.Items)
                .WithOne(s => s.Sale)
                .HasForeignKey(s => s.SaleId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(s => new { s.SaleId, s.ProductId });

            builder.Property(s => s.UnitPrice)
                .HasPrecision(10, 2);

            builder.Property(s => s.Discount)
                .HasPrecision(10, 2);

            builder.HasOne(s => s.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId);
        }
    }
}
