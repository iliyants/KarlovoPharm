namespace KarlovoPharm.Services.Data.Users
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }
        public T GetUserInfo<T>(string userId)
        {

            return this
                .userRepository
                .AllAsNoTracking()
                .Include(x => x.UserAddresses)
                .ThenInclude(x => x.Address)
                .SingleOrDefault(x => x.Id == userId)
                .To<T>();
        }
    }
}
