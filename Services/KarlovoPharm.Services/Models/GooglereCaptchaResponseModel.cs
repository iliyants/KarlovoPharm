namespace KarlovoPharm.Services.Models
{
    using System;

    public class GooglereCaptchaResponseModel
    {
        public bool Success { get; set; }

        public double Score { get; set; }

        public string Action { get; set; }

        public DateTime Challenge_ts { get; set; }

        public string HostName { get; set; }
    }
}
