namespace KarlovoPharm.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum OrderStatus
    {
        [Display(Name = "В процес на обработване")]
        InProccess = 1,

        [Display(Name = "Не обработена")]
        UnProccessed = 2,

        [Display(Name = "Обработена")]
        Proccessed = 3,

        [Display(Name = "Доставена")]
        Delivered = 4,
    }
}
