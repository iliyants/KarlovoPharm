namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ShoppingCartProductConfig : IEntityTypeConfiguration<ShoppingCartProduct>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProduct> scp)
        {
            scp.HasKey(x => new { x.ProductId, x.ShoppingCartId });

            scp.HasOne(x => x.ShoppingCart)
                .WithMany(x => x.ShoppingCartProducts)
                .HasForeignKey(x => x.ShoppingCartId);

            scp.HasOne(x => x.Product)
                .WithMany(x => x.ProductShoppingCarts)
                .HasForeignKey(x => x.ProductId);

            scp.Property(x => x.ShoppingCartId)
                .IsRequired();

            scp.Property(x => x.ProductId)
                .IsRequired();

            scp.Property(x => x.Quantity)
                .HasDefaultValue(1);
        }
    }
}
