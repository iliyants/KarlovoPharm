namespace KarlovoPharm.Services.Data.Categories
{
    using KarlovoPharm.Data.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
    }
}
