namespace KarlovoPharm.Web.ViewModels.Categories
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
