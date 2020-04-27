using KarlovoPharm.Data.Common.Repositories;
using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Data.Addresses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.UsersAddresses
{
    public class UsersAddressesService : IUsersAddressesService
    {
        private readonly IDeletableEntityRepository<UserAddress> userAddressRepository;

        public UsersAddressesService(IDeletableEntityRepository<UserAddress> userAddressRepository)
        {
            this.userAddressRepository = userAddressRepository;
        }

        public async Task<bool> CreateAsync(string userId, string addressId)
        {
            if (userId == null || addressId == null)
            {
                throw new ArgumentNullException("UserId or AddressId were null");
            }

            var userAddress = new UserAddress()
            {
                UserId = userId,
                AddressId = addressId
            };

            await this.userAddressRepository.AddAsync(userAddress);

            var result = await this.userAddressRepository.SaveChangesAsync();

            return result > 0;

        }

        public async Task<bool> DeleteAsync(string addressId, string userId)
        {
            var userAddress = await this.userAddressRepository
                .All()
                .SingleOrDefaultAsync(x => x.AddressId == addressId && x.UserId == userId);

            if (userAddress == null)
            {
                throw new ArgumentNullException("Address was null!");
            }

            this.userAddressRepository.HardDelete(userAddress);

            var result = await this.userAddressRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
