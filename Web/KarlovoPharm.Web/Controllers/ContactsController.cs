namespace KarlovoPharm.Web.Controllers
{
    using System.Threading.Tasks;
    using KarlovoPharm.Common;
    using KarlovoPharm.Services.Messaging;
    using KarlovoPharm.Web.InputModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : BaseController
    {
        private const string EmailSendSuccessMessage = "Вашият имейл беше успешно изпратен!";

        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailCreateInputModel emailCreateInputModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.Redirect(nameof(this.Contact));
            }

            await this.emailSender.SendEmailAsync(
                emailCreateInputModel.Email,
                emailCreateInputModel.Name,
                GlobalConstants.AdminEmail,
                emailCreateInputModel.Subject,
                emailCreateInputModel.Message);

            this.TempData["InfoMessage"] = EmailSendSuccessMessage;

            return this.RedirectToAction(nameof(this.Contact));
        }
    }
}
