namespace KarlovoPharm.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductAllViewModel
    {
        public ProductAllViewModel()
        {
            this.Products = new HashSet<ProductSingleViewModel>();
        }

        public string SubcategoryId { get; set; }

        public IEnumerable<ProductSingleViewModel> Products { get; set; }
    }
}
