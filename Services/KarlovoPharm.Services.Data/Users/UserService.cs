﻿namespace KarlovoPharm.Services.Data.Users
{
    using KarlovoPharm.Data.Common.Repositories;
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

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        private async Task<bool> UserNameIsNotUnique(string userName)
        {
            var userUserName = await this.userRepository.AllAsNoTracking().Select(x => x.UserName).ToListAsync();

            return userUserName.Contains(userName);
        }

        public async Task<bool> UserHasThreeAddresses(string userId)
        {

            var userAdresses = await this.userRepository
                .AllAsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => x.UserAddresses)
                .ToListAsync();

            if (userAdresses.Count == 0)
            {
                return false;
            }
            return userAdresses[0].Count == 3;
        }

        public async Task<bool> EditProfileAsync(ProfileEdintInputModel profileEditInputModel)
        {
            var user = await this.userRepository.All().Where(x => x.Id == profileEditInputModel.Id).SingleOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentNullException("User was null !");
            }

            if (user.UserName != profileEditInputModel.Username &&
                await this.UserNameIsNotUnique(profileEditInputModel.Username))
            {
                return false;
            }

            profileEditInputModel.To(user);

            await this.userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetUserInfo<T>(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("UserId was null");
            }

            var user = await this
                .userRepository
                .AllAsNoTracking()
                .Include(x => x.UserAddresses)
                .ThenInclude(x => x.Address)
                .SingleOrDefaultAsync(x => x.Id == userId);

            return user.To<T>();
        }

        public async Task<ApplicationUser> GetUserWithAllPropertiesById(string userId)
        {

            if (userId == null)
            {
                throw new ArgumentNullException("UserId was null");
            }

            var result =   await this.userRepository.All()
                .Where(x => x.Id == userId)
                .Include(x => x.UserAddresses)
                .ThenInclude(x => x.Address)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.ShoppingCartProducts)
                .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync();


            return result;
        }

    }
}
