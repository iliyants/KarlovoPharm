namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;

    public class ProductSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {


            if (dbContext.Products.Any())
            {
                return;
            }

            var testSubCategoryId = dbContext.SubCategories.Where(x => x.Name == "TestSubCategory")
                .Select(x => x.Id)
                .SingleOrDefault();

            var products = new List<(string Name,
                                     string SubCategoryId,
                                     string Picture,
                                     string Description,
                                     decimal Price,
                                     string Specification,
                                     string Designation,
                                     string Effect,
                                     string Composition,
                                     string WayOfUse,
                                     string Manufacturer,
                                     string ContryOfOrigin
                                     )>
            {
                ("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 2.5m , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 3.89m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 4,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Mask", testSubCategoryId, "https://img.gkbcdn.com/s3/p/2020-03-09/Smartmi-KN95-Anti-haze-Mask-Size-M-Black-899000-.jpg", "Some description that is above 10 symbols", 7.50m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Diclac", testSubCategoryId, "https://apteka-optima.com/images/products/large/diklak-id-tabletki-150-mg-h-50.jpg", "Some description that is above 10 symbols", 20m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Ibuprofen", testSubCategoryId, "https://www.dollargeneral.com/media/catalog/product/cache/0729a8e318a86bbdd225c6c8aa5967a3/0/0/00748101_dghl_ibuprofen_orange_250ct.jpg", "Some description that is above 10 symbols", 4.70m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Reparil-Dragees", testSubCategoryId, "https://www.dowa.co/content/images/thumbs/0002246_reparil-dragees_600.jpeg", "Some description that is above 10 symbols", 8m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Imunobor", testSubCategoryId, "https://static.framar.bg/product/hranitelna-dobavka-za-imunnata-sistema-imunobor.jpg", "Some description that is above 10 symbols", 10m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Coldrex", testSubCategoryId, "https://static.framar.bg/product/coldrex-maxgrip-galeriq.jpg", "Some description that is above 10 symbols", 1.69m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Astera", testSubCategoryId, "https://www.aroma.bg/uploads/catalog/products/169/photo/photo_1139_640_t.png", "Some description that is above 10 symbols", 2.20m,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
            };

            foreach (var product in products)
            {
                await dbContext.Products.AddAsync(new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Picture = product.Picture,
                    SubCategoryId = product.SubCategoryId,
                    Specification = product.Specification,
                    Designation = product.Designation,
                    Effect = product.Effect,
                    Composition = product.Composition,
                    WayOfuse = product.WayOfUse,
                    Manufacturer = product.Manufacturer,
                    CountryOfOrigin = product.ContryOfOrigin,
                });
            }
        }
    }
}
