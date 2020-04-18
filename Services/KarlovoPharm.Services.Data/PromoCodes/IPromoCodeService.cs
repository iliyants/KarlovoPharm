namespace KarlovoPharm.Services.Data.PromoCodes
{
    using KarlovoPharm.Web.InputModels.PromoCodes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPromoCodeService
    {
        public Task<bool> AddAsync(PromoCodeCreateInputModel promoCodeCreateInputModel);

        public Task<IEnumerable<T>> All<T>();

        public Task<bool> EditAsync(PromoCodeCreateInputModel promoCodeCreateInputModel);

        public Task<T> GetByIdAsync<T>(string promoCodeId);

        public Task<bool> DeleteAsync(string promocodeId);

        public Task<bool> ExistsAsync(string promoCodeName);

        public Task<decimal> DeductPercentageFromPrice(decimal price, string promoCodeId);

        public Task<string> GetIdByNameAsync(string promoCodeName);
    }
}
