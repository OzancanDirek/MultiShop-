using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.StatisticServices.CatalogStatisticServices;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class StatisticsController : ControllerBase //these controller helps to get statistics about the catalog.
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("GetBrandCount")]
        public async Task<IActionResult> GetBrandCount()
        {
            var value = await _statisticService.GetBrandCount();
            return Ok(value);
        }

        [HttpGet("GetCategoryCount")]
        public async Task<IActionResult> GetCategoryCount()
        {
            var value = await _statisticService.GetCategoryCount();
            return Ok(value);
        }

        [HttpGet("GetProductCount")]
        public async Task<IActionResult> GetProductCount()
        {
            var value = await _statisticService.GetProductCount();
            return Ok(value);
        }

        [HttpGet("GetProductAvgPrice")] // This endpoint returns the average price of all products in the catalog.
        public async Task<IActionResult> GetProductAvgPrice()
        {
            var value = await _statisticService.GetProductAvgPrice();
            return Ok(value);
        }

        [HttpGet("GetMaxPriceProductName")] // This endpoint returns the name of the product with the highest price in the catalog.
        public async Task<IActionResult> GetMaxPriceProductName()
        {
            var value = await _statisticService.GetMaxPriceProductName();
            return Ok(value);
        }

        [HttpGet("GetMinPriceProductName")] // This endpoint returns the name of the product with the lowest price in the catalog.
        public async Task<IActionResult> GetMinPriceProductName()
        {
            var value = await _statisticService.GetMinPriceProductName();
            return Ok(value);
        }
    }
}

//TODO: Add more statistics endpoints as needed, such as getting the total revenue, average rating, etc.
