using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.UsersAddresses
{
    public interface IUsersAddressesService
    {
        Task<bool> CreateAsync(string userId, string addressId);

        Task<bool> DeleteAsync(string addressId);
    }
}
