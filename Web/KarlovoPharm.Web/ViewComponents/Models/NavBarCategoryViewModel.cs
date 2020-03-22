namespace KarlovoPharm.Web.ViewComponents.Models
{
    using System.Collections.Generic;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.Categories;

    public class NavBarCategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}
