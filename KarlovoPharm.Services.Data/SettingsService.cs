namespace KarlovoPharm.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using KarlovoPharm.Data.Common.Repositories;
    using KarlovoPharm.Data.Models.Common;
    using KarlovoPharm.Services.Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}