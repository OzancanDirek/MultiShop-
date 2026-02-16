using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.OrderDtos.OrderAdressDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.OrderServices.OrderAdressServices;

namespace MultiShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAdressService _orderAdressService;
        private readonly IUserService _userService;

        public OrderController(IOrderAdressService orderAdressService, IUserService userService)
        {
            _orderAdressService = orderAdressService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Siparişler";
            ViewBag.directory3 = "Sipariş İşlemleri";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Index(CreateOrderAdressDto createOrderAdressDto)
        {
            var values = await _userService.GetUserInfo();
            createOrderAdressDto.UserId = values.Id;
            createOrderAdressDto.Description = "aa";
            await _orderAdressService.CreateOrderAdressAsync(createOrderAdressDto);
            return RedirectToAction("Index", "Payment");
        }
    }
}
