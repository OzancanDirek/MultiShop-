namespace MultiShop.WebUI.Services.Interfaces
{
    public interface IClientCredentialService
    {
        Task<string> GetToken();
    }
}
