using Budget.MVC.App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Budget.MVC.App.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }
    }
}