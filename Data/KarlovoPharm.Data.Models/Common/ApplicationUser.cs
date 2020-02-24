// ReSharper disable VirtualMemberCallInConstructor
namespace KarlovoPharm.Data.Models.Common
{
    using System;
    using System.Collections.Generic;

    using KarlovoPharm.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.UserAddresses = new HashSet<UserAddress>();
            this.FavouriteProducts = new HashSet<UserFavouriteProduct>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }

        public ICollection<UserFavouriteProduct> FavouriteProducts { get; set; }
    }
}
