using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.Users
{
    public interface IUserService
    {
        public T GetUserInfo<T>(string userId);
    }
}
