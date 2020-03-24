namespace KarlovoPharm.Data.EntityConfigurations
{

    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserAdressConfig : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> userAddress)
        {
            userAddress.HasKey(x => new { x.UserId, x.AddressId });

            userAddress.HasOne(x => x.Address)
                .WithMany(x => x.AddressUsers)
                .HasForeignKey(x => x.AddressId);

            userAddress.HasOne(x => x.User)
                .WithMany(x => x.UserAddresses)
                .HasForeignKey(x => x.UserId);

            userAddress.Property(x => x.UserId)
                .IsRequired();

            userAddress.Property(x => x.AddressId)
                .IsRequired();
        }
    }
}
