namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PromoCodeConfig : IEntityTypeConfiguration<PromoCode>
    {
        public void Configure(EntityTypeBuilder<PromoCode> promoCode)
        {
            promoCode.HasKey(x => x.Id);

            promoCode.Property(x => x.Id)
      .ValueGeneratedOnAdd();

            promoCode.Property(x => x.Name)
                .IsRequired();

            promoCode.Property(x => x.DiscountInPercentage)
                .IsRequired();
        }
    }
}
