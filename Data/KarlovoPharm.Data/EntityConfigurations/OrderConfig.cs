namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order.HasKey(x => x.Id);

            order.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            order.Property(x => x.UserId)
                .IsRequired();
        }
    }
}
