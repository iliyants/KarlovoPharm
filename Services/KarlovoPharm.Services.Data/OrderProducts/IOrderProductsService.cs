using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.OrderProducts
{
    public interface IOrderProductsService
    {
        Task DeleteAll(string orderId);
    }
}
