using System.Collections.Generic;
using System.Diagnostics;
using DateBaseServices;
using DateBaseServices.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotionV3.Models;
using NotionV3.Utils;

namespace NotionV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = new DataContext();
        }

        [HttpGet]
        public IActionResult Index([FromQuery] int catId = -1)
        {
            var notes = new List<Note>();
            var user = UserCookieUtility.GetUserInfoFromCookies(HttpContext);
            if (user.UserId == -1)
                return View(notes);

            if (catId == -1)
                return View(notes);

            notes = _db.Notes.GetNotesByCatIdForUserByUserId(catId, user.UserId, user.Token);
            return View(notes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}