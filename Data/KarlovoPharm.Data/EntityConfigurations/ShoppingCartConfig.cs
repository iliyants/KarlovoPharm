namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShoppingCartConfig : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> shoppingCart)
        {
            shoppingCart.HasKey(x => x.Id);

            shoppingCart.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            shoppingCart.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
