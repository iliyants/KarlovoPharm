namespace KarlovoPharm.Services.Data.Users
{
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Web.InputModels.Users;
    using System.Threading.Tasks;
    public interface IUserService
    {
        public T GetUserInfo<T>(string userId);

        Task<bool> EditProfileAsync(ProfileEdintInputModel productEditInputModel);

        Task<bool> UserHasThreeAddresses(string userId);

        Task<ApplicationUser> GetUserWithAllPropertiesById(string userId);

    }
}
