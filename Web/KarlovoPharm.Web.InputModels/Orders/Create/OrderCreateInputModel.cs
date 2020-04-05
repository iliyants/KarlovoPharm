namespace KarlovoPharm.Web.InputModels.Orders.Create
{
    using AutoMapper;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Enums;
    using KarlovoPharm.Services.Mapping;
    public class OrderCreateInputModel : IMapTo<Order>, IHaveCustomMappings
    {

        public string Id { get; set; }

        public string Recipient { get; set; }

        public string RecipientLastName { get; set; }

        public string RecipientPhoneNumber { get; set; }

        public string OfficeAddress { get; set; }

        public string DeliveryAddressId { get; set; }

        public PaymentType PaymentType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<OrderCreateInputModel, Order>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
