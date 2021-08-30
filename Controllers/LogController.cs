using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogError("Logando erro.");

            return View();
        }
    }
}
