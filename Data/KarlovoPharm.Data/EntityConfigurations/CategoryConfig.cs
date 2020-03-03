namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.HasKey(x => x.Id);

            category.HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);

            category.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            category.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            category
                .HasIndex(x => x.Name)
                .IsUnique();

        }
    }
}
