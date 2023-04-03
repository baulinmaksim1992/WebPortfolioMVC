using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebPortfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Electrician()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}