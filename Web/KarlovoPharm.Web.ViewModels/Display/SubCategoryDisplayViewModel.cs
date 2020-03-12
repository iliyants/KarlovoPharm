namespace KarlovoPharm.Web.ViewModels.Display
{
    using AutoMapper;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Categories.Display;

    public class SubCategoryDisplayViewModel : IMapFrom<SubCategory>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CategoryDisplayInputModel Category { get; set; }

        public int ProductsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SubCategoryDisplayViewModel, SubCategory>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom((src, dest) => src.ProductsCount));
        }
    }
}
