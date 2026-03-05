using Microsoft.AspNetCore.Mvc.Localization;

namespace MultiShop.WebUI.Services
{
    public class LocalizationService
    {
        private readonly IViewLocalizer _localizer;

        public LocalizationService(IViewLocalizer localizer)
        {
            _localizer = localizer;
        }

        public string GetLocalizationHtmlString(string key)
        {
            return _localizer[key].Value;
        }
    }
}