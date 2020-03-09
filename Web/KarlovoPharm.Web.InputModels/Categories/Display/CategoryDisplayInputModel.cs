using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Mapping;

namespace KarlovoPharm.Web.InputModels.Categories.Display
{
    public class CategoryDisplayInputModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
