﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace KarlovoPharm.Services.Data.OrderProducts
{
    public interface IOrderProductsService
    {
        Task<bool> DeleteAll(string orderId);

        Task<IEnumerable<T>> MostPurchased<T>();

    }
}
