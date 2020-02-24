namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> orderProduct)
        {
            orderProduct.HasKey(x => new { x.OrderId, x.ProductId });

            orderProduct.HasOne(x => x.Order)
                .WithMany(x => x.OrderProducts)
                .HasForeignKey(x => x.OrderId);

            orderProduct.HasOne(x => x.Product)
                .WithMany(x => x.ProductOrders)
                .HasForeignKey(x => x.ProductId);

            orderProduct.Property(x => x.OrderId)
                .IsRequired();

            orderProduct.Property(x => x.ProductId)
                .IsRequired();
        }
    }
}
