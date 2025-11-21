namespace MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands
{
    public class RemoveAdressCommand
    {
        public int Id { get; set; }

        public RemoveAdressCommand(int id)
        {
            Id = id;
        }
    }
}
