namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> product)
        {
            product.HasKey(x => x.Id);

            product.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            product.Property(x => x.Specification)
                .HasMaxLength(500);

            product.Property(x => x.Price)
                .IsRequired();

            product.Property(x => x.Picture)
                .IsRequired();

            product.Property(x => x.Available)
                .IsRequired();

            product.Property(x => x.SubCategoryId)
                .IsRequired();
        }
    }
}
