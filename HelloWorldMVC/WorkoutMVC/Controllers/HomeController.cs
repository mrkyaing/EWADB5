using Microsoft.AspNetCore.Mvc;

namespace WorkoutMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
