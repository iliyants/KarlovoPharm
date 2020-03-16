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

            product.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            product.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            product.Property(x => x.Price)
                .IsRequired();

            product.Property(x => x.Picture)
                .IsRequired();

            product.Property(x => x.Available)
                .HasDefaultValue(true);

            product.Property(x => x.SubCategoryId)
                .IsRequired();

            product.Property(x => x.Designation)
                .HasMaxLength(500);

            product.Property(x => x.Effect)
                .HasMaxLength(500);

            product.Property(x => x.Composition)
                .HasMaxLength(500);

            product.Property(x => x.WayOfuse)
                .HasMaxLength(500);

            product.Property(x => x.Specification)
                .HasMaxLength(500);

            product.Property(x => x.Manufacturer)
                .HasMaxLength(50)
                .IsRequired();

            product.Property(x => x.CountryOfOrigin)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
