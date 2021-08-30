using AspNetCoreMVC.Extensions;
using AspNetCoreMVC.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Debug("Acessando a página principal");

            return View();
        }

        public IActionResult Privacy()
        {
            throw new System.Exception("Exception!");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ManageApplication()
        {
            return View();
        }

        [Authorize(Policy = "CanStopApplication")]
        public IActionResult StopApplication()
        {
            return View("ManageApplication");
        }

        [Authorize(Policy = "CanStart")]
        public IActionResult StartApplication()
        {
            return View("ManageApplication");
        }

        [ClaimsAuthorization("Home", "GET")]
        public IActionResult RestartApplication()
        {
            return View("ManageApplication");
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var error = new ErrorViewModel();

            if (id == 500)
            {
                error.ErrorCode = id;
                error.Titulo = "Internal Server Error";
                error.Mensagem = "Casa caiu no servidor";
            }
            else if (id == 404)
            {
                error.ErrorCode = id;
                error.Titulo = "Not Found";
                error.Mensagem = "Sumiuu";
            }
            else if (id == 403)
            {
                error.ErrorCode = id;
                error.Titulo = "Forbidden";
                error.Mensagem = "Ai não champz";
            }
            else
                return StatusCode(404);

            return View("Error", error);
        }
    }
}
