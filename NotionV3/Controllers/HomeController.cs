using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotionV3.Models;

namespace NotionV3.Controllers
{
    using DateBaseServices.Models;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public PartialViewResult getMenuList()
        {
            var list = new List<Category>
            {
                new Category{ CategoryId = 1, Title = "first cat"},
                new Category{ CategoryId = 2, Title = "Sec cat"},
            };

            return PartialView("getMenuList", list);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}