using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.SliderServices;

namespace MultiShop.WebUI.ViewComponents.DefaultViiewComponent
{
    public class _CarouselDefaultComponentPartial : ViewComponent
    {
        private readonly IFeatureSliderService _featureSliderService;

        public _CarouselDefaultComponentPartial(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featureSliderService.GetAllFeatureAsync();
            return View(values);
        }
    }
}
