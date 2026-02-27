using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticServices _catalogStatisticServices;
        private readonly IUserStatisticServices _userStatisticServices;
        private readonly ICommentService _commentService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticServices _messageStatisticServices;

        public StatisticController
            (
            ICatalogStatisticServices catalogStatisticServices,
            IUserStatisticServices userStatisticServices,
            ICommentService commentService,
            IDiscountStatisticService discountStatisticService,
            IMessageStatisticServices messageStatisticServices)
        {
            _catalogStatisticServices = catalogStatisticServices;
            _userStatisticServices = userStatisticServices;
            _commentService = commentService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticServices = messageStatisticServices;
        }

        public async Task<IActionResult> Index()
        {
            var getBrandCount = await _catalogStatisticServices.GetBrandCount();
            var getProductCount = await _catalogStatisticServices.GetProductCount();
            var getCategoryCount = await _catalogStatisticServices.GetCategoryCount();
            var getMaxPriceProductName = await _catalogStatisticServices.GetMaxPriceProductName();
            var getMinPriceProductName = await _catalogStatisticServices.GetMinPriceProductName();
            var getProductAvgPrice = await _catalogStatisticServices.GetProductAvgPrice();
            var getActiveCommentCount = await _commentService.GetActiveCommentCount();
            var getPassiveCommentCount = await _commentService.GetPassiveCommentCount();
            var gettotalCommentCount = await _commentService.GetTotalCommentCount();

            var getUserCount = await _userStatisticServices.GetUserCount();
            var getDiscountCount = await _discountStatisticService.GetDiscountCouponCount();
            var getMessageCount = await _messageStatisticServices.GetTotalMessageCount();

            //Products
            ViewBag.brandCount = getBrandCount;
            ViewBag.getProductCount = getProductCount;
            ViewBag.getCategoryCount = getCategoryCount;
            ViewBag.getMaxPriceProductName = getMaxPriceProductName;
            ViewBag.getMinPriceProductName = getMinPriceProductName;
            ViewBag.getProductAvgPrice = getProductAvgPrice;

            //Comments
            ViewBag.getActiveCommentCount = getActiveCommentCount;
            ViewBag.getPassiveCommentCount = getPassiveCommentCount;
            ViewBag.gettotalCommentCount = gettotalCommentCount;

            //Discounts
            ViewBag.getDiscountCount = getDiscountCount;

            //Users
            ViewBag.getUserCount = getUserCount;

            //Messages
            ViewBag.getMessageCount = getMessageCount;
            return View();
        }
    }
}

