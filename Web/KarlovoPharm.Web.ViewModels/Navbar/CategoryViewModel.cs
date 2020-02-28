namespace KarlovoPharm.Web.ViewModels.Navbar
{
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<CategoryViewModel> SubCategories { get; set; }
    }
}
