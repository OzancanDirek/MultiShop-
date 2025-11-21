namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands
{
    public class CreateOrderDetailCommand
    {
        public int ProductId { get; set; }
        public int ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public decimal ProductTotalPrice { get; set; }
        public int OrderingId { get; set; }
    }
}
