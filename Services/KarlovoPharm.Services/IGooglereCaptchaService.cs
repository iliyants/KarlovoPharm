namespace KarlovoPharm.Services
{
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Models;

    public interface IGooglereCaptchaService
    {
        public Task<GooglereCaptchaResponseModel> GooglereCaptchaVerification(string token);
    }
}
