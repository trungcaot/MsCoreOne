using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MsCoreOne.Domain.Entities;

namespace MsCoreOne.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
