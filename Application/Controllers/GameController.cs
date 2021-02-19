using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IUserService _userService;
        private readonly string pattern;
        public GameController(ITableService tableService, IUserService userService) 
        {
            _tableService = tableService;
            _userService = userService;
            pattern = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        }
        public IActionResult Index()
        {
            ViewData["Error"] = TempData.Peek("error");
            var temp = _tableService.ReturnAllTables();
            return View(_tableService.ReturnAllTables());
        }

        [HttpPost]
        public IActionResult CreateNewTable(string name)
        {
            if (name != null)
            {
                if (_tableService.IsTableNameUniq(name))
                {
                    var temp = _tableService.CreateTable(name);
                    return RedirectToAction("Room", new { name });
                }
                else
                {
                    TempData["error"] = "Stół o podanej nazwie już istnieje";
                }
            }
            else
            {
                TempData["error"] = "Pole z nazwą nie może być puste";
            }
            return RedirectToAction("Index");
        }

        public IActionResult Room(string name)
        {
            var email = HttpContext.User.Claims.First(x => x.Type.Equals(pattern))?.Value;
            var user = _userService.ReturnUser(email);
            ViewBag.Table = name;
            return View(user);
        }
    }
}