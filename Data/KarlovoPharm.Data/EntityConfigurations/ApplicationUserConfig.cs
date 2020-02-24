namespace KarlovoPharm.Data.EntityConfigurations
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> user)
        {
            user
           .HasMany(e => e.Claims)
           .WithOne()
           .HasForeignKey(e => e.UserId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            user
            .HasMany(e => e.Logins)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            user
           .HasMany(e => e.Roles)
           .WithOne()
           .HasForeignKey(e => e.UserId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            user
            .HasOne(x => x.ShoppingCart)
            .WithOne(x => x.User)
            .HasForeignKey<ShoppingCart>(x => x.UserId);
        }
    }
}
