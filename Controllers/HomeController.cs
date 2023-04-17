using Microsoft.AspNetCore.Mvc;
using musicShop.Models;
using System.Diagnostics;

namespace musicShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string userId = ((System.Security.Principal.IIdentity)HttpContext.User.Identity).Name;
            if (userId != null)
                ViewBag.IsUser = "true";
            else
                ViewBag.IsUser = "false";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}