using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    public class MyOrderController : Controller
    {
        [Area("User")]
        public IActionResult MyOrderList()
        {
            return View();
        }
    }
}
