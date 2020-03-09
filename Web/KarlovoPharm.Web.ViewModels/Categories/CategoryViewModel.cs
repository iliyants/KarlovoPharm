namespace KarlovoPharm.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {

        public CategoryViewModel()
        {
            this.SubCategories = new HashSet<SubCategoryViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }
    }
}
