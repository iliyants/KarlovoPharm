namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserFavouriteProductConfig : IEntityTypeConfiguration<UserFavouriteProduct>
    {
        public void Configure(EntityTypeBuilder<UserFavouriteProduct> ufp)
        {
            ufp.HasKey(x => new { x.UserId, x.ProductId });

            ufp.HasOne(x => x.User)
                .WithMany(x => x.FavouriteProducts)
                .HasForeignKey(x => x.UserId);

            ufp.Property(x => x.UserId)
                .IsRequired();

            ufp.Property(x => x.ProductId)
                .IsRequired();
        }
    }
}
