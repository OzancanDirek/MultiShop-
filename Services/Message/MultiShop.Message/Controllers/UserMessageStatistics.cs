using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageStatistics : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessageStatistics(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTotalMessageCount()
        {
            int messageCount = await _userMessageService.GetTotalMessageCount();
            return Ok(messageCount);
        }
    }
}
