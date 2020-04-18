namespace KarlovoPharm.Web.InputModels.Orders.Create
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;
    public class OrderCreateInputModel : IMapTo<Order>
    {

        public string UserId { get; set; }

        public string Recipient { get; set; }

        public string PromoCodeId { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string OfficeAddress { get; set; }

        public string DeliveryAddressId { get; set; }

        public PaymentType PaymentType { get; set; }

    }
}
