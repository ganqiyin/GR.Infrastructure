using GR.EfCore.Tests.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GR.EfCore.Tests.Map
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
        }
    }
}
