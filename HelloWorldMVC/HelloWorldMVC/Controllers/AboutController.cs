using Microsoft.AspNetCore.Mvc;

namespace HelloWorldMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
