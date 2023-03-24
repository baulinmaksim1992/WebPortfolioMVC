using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebPortfolioMVC.Models;

namespace WebPortfolioMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}