namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> address)
        {
            address.HasKey(x => x.Id);

            address.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            address.Property(x => x.Description)
                .HasMaxLength(100);

            address.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(20);

            address.Property(x => x.Street)
               .IsRequired()
               .HasMaxLength(40);

            address.Property(x => x.BuildingNumber)
               .IsRequired()
               .HasMaxLength(10);
        }
    }
}
