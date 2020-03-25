namespace KarlovoPharm.Services.Data.Users
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        private async Task<bool> UserNameIsNotUnique(string userName)
        {
            var userUserName = await this.userRepository.AllAsNoTracking().Select(x => x.UserName).ToListAsync();

            return userUserName.Contains(userName);
        }

        public async Task<bool> EditProfileAsync(ProfileEdintInputModel profileEditInputModel)
        {
            var user = await this.userRepository.All().Where(x => x.Id == profileEditInputModel.Id).SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("User was null !");
            }

            if (user.UserName != profileEditInputModel.Username &&
                await this.UserNameIsNotUnique(profileEditInputModel.Username))
            {
                return false;
            }

            profileEditInputModel.To(user);

            await this.userManager.UpdateAsync(user);

            return true;
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
