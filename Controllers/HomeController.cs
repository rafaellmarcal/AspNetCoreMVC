﻿using AspNetCoreMVC.Extensions;
using AspNetCoreMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspNetCoreMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
