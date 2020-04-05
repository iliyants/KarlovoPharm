namespace KarlovoPharm.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PaymentType
    {
        [Display(Name = "Наложен платеж")]
        CashOnDelivery = 1,

        [Display(Name = "В брой (EasyPay)")]
        EasyPay = 2,
    }
}
