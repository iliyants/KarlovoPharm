namespace KarlovoPharm.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Data.PromoCodes;
    using KarlovoPharm.Web.InputModels.PromoCodes;
    using KarlovoPharm.Web.ViewModels.PromoCodes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PromoCodesController : AdministrationController
    {
        private const string NameNotUniqueErrorMessage = "Вече съществува промокод с това име!";

        private const string DeleteAttemptErrorMessage = "Възникна грешка при опит за изтриване на промокод.";

        private readonly IPromoCodeService promoCodeService;

        public PromoCodesController(IPromoCodeService promoCodeService)
        {
            this.promoCodeService = promoCodeService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(PromoCodeCreateInputModel promoCodeCreateInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Add));
            }
 
            if (!await this.promoCodeService.AddAsync(promoCodeCreateInputModel))
            {
                this.TempData["Error"] = NameNotUniqueErrorMessage;
                return this.RedirectToAction(nameof(this.Add));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var promoCodes = await this.promoCodeService.All<PromoCodeViewModel>();

            return this.View(promoCodes);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string promoCodeId)
        {
            var promoCodeCreateInputModel = await this.promoCodeService.GetByIdAsync<PromoCodeCreateInputModel>(promoCodeId);
            return this.View(promoCodeCreateInputModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(PromoCodeCreateInputModel promoCodeCreateInputModel)
        {
            if (!await this.promoCodeService.EditAsync(promoCodeCreateInputModel))
            {
                this.TempData["Error"] = NameNotUniqueErrorMessage;

                return this.RedirectToAction(nameof(this.Edit), new { promoCodeId = promoCodeCreateInputModel.Id });
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string promoCodeId)
        {
            if (!await this.promoCodeService.DeleteAsync(promoCodeId))
            {
                this.TempData["Error"] = DeleteAttemptErrorMessage;
                return this.RedirectToAction(nameof(this.All));
            }

            return this.RedirectToAction(nameof(this.All));

        }
    }
}
