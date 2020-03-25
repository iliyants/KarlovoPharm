namespace KarlovoPharm.Web.ViewComponents
{
    using KarlovoPharm.Web.ViewComponents.Models;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "InfoMessage")]
    public class InfoMessageComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            var viewModel = new InfoMessageViewModel()
            {
                Status = message,
            };

            return this.View(viewModel);
        }
    }
}
