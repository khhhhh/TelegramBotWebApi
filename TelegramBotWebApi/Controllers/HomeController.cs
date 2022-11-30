using Microsoft.AspNetCore.Mvc;

namespace TelegramBotWebApi.Controllers
{
    public class HomeController : ControllerBase
    {

        [HttpGet("test")]
        public string Index()
        {
            return "Test";
        }
    }
}
