using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using agl_code_challenge.Services;

namespace agl_code_challenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeopleService _service;

        public HomeController(IPeopleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _service.GetCatListByOwnerGender();
                return View("Index", data);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error(string description)
        {
            ViewData["Message"] = description;
            return View("Error");
        }
    }
}