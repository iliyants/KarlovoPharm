namespace KarlovoPharm.Data.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum OrderStatus
    {
        [Display(Name = "Не обработена")]
        UnProccessed = 1,

        [Display(Name = "Обработена")]
        Proccessed = 2,

        [Display(Name = "Доставена")]
        Delivered = 3,
    }
}
