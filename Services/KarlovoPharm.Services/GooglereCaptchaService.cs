namespace KarlovoPharm.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using KarlovoPharm.Services.Models;
    using Newtonsoft.Json;

    public class GooglereCaptchaService : IGooglereCaptchaService
    {

        private readonly string apiSecret;
        private readonly HttpClient client;

        public GooglereCaptchaService(string apiSecret)
        {
            this.apiSecret = apiSecret;
            this.client = new HttpClient();
        }

        public async Task<GooglereCaptchaResponseModel> GooglereCaptchaVerification(string token)
        {

            var response = await this.client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={this.apiSecret}&response={token}");

            var result = JsonConvert.DeserializeObject<GooglereCaptchaResponseModel>(response);

            return result;

        }
    }
}
