using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.DiscountServices;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ComfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ComfirmDiscountCoupon(string code)
        {
            const decimal taxRate = 0.10m;

            var discountRate = await _discountService.GetDiscountCouponRateAsync(code);
            var basketValues = await _basketService.GetBasket();

            if (basketValues == null || basketValues.TotalPrice <= 0)
                return RedirectToAction("Index", "ShoppingCard");

            var tax = basketValues.TotalPrice * taxRate;
            var totalPriceWithTax = basketValues.TotalPrice + tax;

            var discountAmount = totalPriceWithTax * discountRate / 100m;
            var totalNewPriceWithDiscount = totalPriceWithTax - discountAmount;

            return RedirectToAction("Index", "ShoppingCard", new
            {
                code = code,
                discountRate = discountRate,
                totalNewPriceWithDiscount = totalNewPriceWithDiscount
            });
        }
    }
}
