namespace KarlovoPharm.Services.Data.Addresses
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Web.InputModels.Addresses.Create;
    using KarlovoPharm.Web.InputModels.Addresses.Edit;
    using System.Threading.Tasks;
    public interface IAddressService
    {
        public Task<Address> CreateAsync(AddressCreateInputModel addressEditInputModel);

        public T GetById<T>(string addressId);

        public Task<bool> Edit(AddressEditInputModel addressEditInputModel);

        public Task<bool> DeleteAsync(string addressId);

    }
}
