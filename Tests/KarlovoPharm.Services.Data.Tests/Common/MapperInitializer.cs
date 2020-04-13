namespace KarlovoPharm.Services.Data.Tests.Common
{
    using System.Reflection;

    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using KarlovoPharm.Web.ViewModels;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
               typeof(ErrorViewModel).GetTypeInfo().Assembly,
               typeof(ProductCreateInputModel).GetTypeInfo().Assembly);
        }
    }
}
