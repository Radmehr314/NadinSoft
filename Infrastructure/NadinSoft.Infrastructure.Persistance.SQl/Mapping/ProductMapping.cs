using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NadinSoft.Domain.Models.Products;

namespace NadinSoft.Infrastructure.Persistance.SQl.Mapping;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products").HasKey(f => f.Id);
        builder.HasOne(f => f.User).WithMany(f => f.Products).HasForeignKey(f => f.UserId);
        
        // Make ManufactureEmail unique
        builder.HasIndex(f => f.ManufactureEmail).IsUnique();
        
        // Make ProducedDate unique
        builder.HasIndex(f => f.ProducedDate).IsUnique();
        
        // Configure required properties
        builder.Property(f => f.Name).IsRequired();
        builder.Property(f => f.ManufacturePhone).IsRequired();
        builder.Property(f => f.ManufactureEmail).IsRequired();
        builder.Property(f => f.ProducedDate).IsRequired();
    }
}