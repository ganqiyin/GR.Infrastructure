using GR.EfCore.Tests.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GR.EfCore.Tests.Map
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar");
        }
    }
}
