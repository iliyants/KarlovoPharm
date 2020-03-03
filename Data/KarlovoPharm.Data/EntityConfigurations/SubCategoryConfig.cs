namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SubCategoryConfig : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> subCategory)
        {
            subCategory.HasKey(x => x.Id);

            subCategory.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();

            subCategory.Property(x => x.CategoryId)
                .IsRequired();

            subCategory.Property(x => x.Name).IsRequired()
                .HasMaxLength(30);

            subCategory.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

            subCategory.HasIndex(x => x.Name)
                .IsUnique();

        }
    }
}
