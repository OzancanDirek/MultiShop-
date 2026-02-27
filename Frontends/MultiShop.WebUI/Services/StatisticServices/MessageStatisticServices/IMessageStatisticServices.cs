namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices
{
    public interface IMessageStatisticServices
    {
        Task<int> GetTotalMessageCount();
    }
}
