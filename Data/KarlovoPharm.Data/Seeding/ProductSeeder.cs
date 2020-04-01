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
                ("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),("Benalgin Rapid", testSubCategoryId, "https://static.framar.bg/product/benalgin-rapid-10sachets.jpg", "Some description that is above 10 symbols", 10 , "Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Analgin 500 mg", testSubCategoryId, "https://sopharmacy.bg/userfiles/productlargeimages/product_23106.jpg", "Some description that is above 10 symbols", 20,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
                ("Durex Condom", testSubCategoryId, "https://neffassociates.files.wordpress.com/2011/11/durex-condoms.jpg?w=590", "Some description that is above 10 symbols", 30,"Some Specification", "Some Designation", "Some Effect", "Some Composition", "Some WayOfUse", "Some Manufacturer", "Some Country"),
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
