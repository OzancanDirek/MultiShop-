namespace MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands
{
    public class UpdateAdressCommand
    {
        public int AdressId { get; set; }
        public string UserId { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }
    }
}
