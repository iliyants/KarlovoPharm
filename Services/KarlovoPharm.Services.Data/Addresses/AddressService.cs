namespace KarlovoPharm.Services.Data.Addresses
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Addresses.Create;
    using KarlovoPharm.Web.InputModels.Addresses.Edit;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressService(IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async Task<Address> CreateAsync(AddressCreateInputModel addressCreateInputModel )
        {
            if (addressCreateInputModel.City == null ||
                addressCreateInputModel.Street == null ||
                addressCreateInputModel.PostCode == null)
            {
                throw new ArgumentNullException("Some of the adress properties were null.");
            }

            var address = addressCreateInputModel.To<Address>();

            await this.addressRepository.AddAsync(address);

            return address;

        }

        public async Task<bool> DeleteAsync(string addressId)
        {
            var address = await this.addressRepository.AllAsNoTracking().SingleOrDefaultAsync(x => x.Id == addressId);

            if (address == null)
            {
                throw new ArgumentNullException("Product was null !");
            }

            this.addressRepository.HardDelete(address);

            var result = await this.addressRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> Edit(AddressEditInputModel addressEditInputModel)
        {
            var address = await this.addressRepository.All().SingleOrDefaultAsync(x => x.Id == addressEditInputModel.Id);

            addressEditInputModel.To(address);

            var result = await this.addressRepository.SaveChangesAsync();

            return result > 0;
        }

        public T GetById<T>(string addressId)
        {
            return this.addressRepository
                .AllAsNoTracking()
                .SingleOrDefault(x => x.Id == addressId)
                .To<T>();
        }
    }
}
