namespace KarlovoPharm.Web.Components
{
    using KarlovoPharm.Web.Components.Models;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "ErrorAlert" )]
    public class ErrorAlertViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string message)
        {
            var viewModel = new ErrorAlertViewModel()
            {
                ErrorMessage = message,
            };

            return this.View(viewModel);
        }
    }
}
