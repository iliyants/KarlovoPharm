namespace KarlovoPharm.Services.Data.PromoCodes
{
    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.PromoCodes;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PromoCodeService : IPromoCodeService
    {
        private readonly IDeletableEntityRepository<PromoCode> promoCodeRepository;

        public PromoCodeService(IDeletableEntityRepository<PromoCode> promoCodeRepository)
        {
            this.promoCodeRepository = promoCodeRepository;
        }


        private async Task<bool> PromoCodeNameIsNotUnique(string name)
        {
            var promoCodes = await this.promoCodeRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            return promoCodes.Contains(name);
        }
        public async Task<bool> AddAsync(PromoCodeCreateInputModel promoCodeCreateInputModel)
        {
            if (promoCodeCreateInputModel.Name == null || promoCodeCreateInputModel.DiscountInPercentage <= 0)
            {
                throw new ArgumentNullException("Name was null or discount percentage is equal or less than zero.");
            }

            if (await this.PromoCodeNameIsNotUnique(promoCodeCreateInputModel.Name))
            {
                return false;
            }

            var promoCode = new PromoCode();

            promoCodeCreateInputModel.To(promoCode);

            await this.promoCodeRepository.AddAsync(promoCode);

            await this.promoCodeRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> All<T>()
        {
            var promoCodes = await this.promoCodeRepository.AllAsNoTracking().ToListAsync();

            return promoCodes.To<T>();

        }


        public async Task<bool> EditAsync(PromoCodeCreateInputModel promoCodeCreateInputModel)
        {
            var promoCode = await this.promoCodeRepository.All().FirstOrDefaultAsync(x => x.Id == promoCodeCreateInputModel.Id);

            if (promoCode == null || promoCode.Name == null)
            {
                throw new ArgumentNullException("PromoCode id or name was null was null");
            }

            if (await this.PromoCodeNameIsNotUnique(promoCodeCreateInputModel.Name))
            {
                return false;
            }

            promoCodeCreateInputModel.To(promoCode);

            await this.promoCodeRepository.SaveChangesAsync();

            return true;
        }


        public async Task<T> GetByIdAsync<T>(string promoCodeId)
        {
            var promoCode = await this.promoCodeRepository
             .AllAsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == promoCodeId);

            if (promoCode == null)
            {
                throw new ArgumentNullException("PromoCode was null.");
            }

            return promoCode.To<T>();
        }

        public async Task<bool> DeleteAsync(string promoCodeId)
        {
            var promoCode = await this.promoCodeRepository
           .All()
           .SingleOrDefaultAsync(x => x.Id == promoCodeId);

            if (promoCode == null)
            {
                throw new ArgumentNullException("PromoCode was null.");
            }

            this.promoCodeRepository.HardDelete(promoCode);

            var result = await this.promoCodeRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> ExistsAsync(string promoCodeName)
        {
            var promoCodeIds = await this.promoCodeRepository.AllAsNoTracking().Select(x => x.Name).ToListAsync();

            return promoCodeIds.Contains(promoCodeName);
        }

        public async Task<decimal> DeductPercentageFromPrice(decimal price, string promoCodeId)
        {
            var promoCodePercentage = await this.promoCodeRepository
                 .AllAsNoTracking()
                 .Where(x => x.Id == promoCodeId)
                 .Select(x => x.DiscountInPercentage)
                 .SingleOrDefaultAsync();

            decimal finalPrice = price - (price * promoCodePercentage / 100);

            return finalPrice;
        }

        public async Task<string> GetIdByNameAsync(string promoCodeName)
        {
            var promoCodeId = await this.promoCodeRepository.AllAsNoTracking()
                .Where(x => x.Name == promoCodeName).Select(x => x.Id).SingleOrDefaultAsync();

            if (promoCodeId == null)
            {
                throw new ArgumentNullException("Promocode was null.");
            }

            return promoCodeId;

        }
    }
}
