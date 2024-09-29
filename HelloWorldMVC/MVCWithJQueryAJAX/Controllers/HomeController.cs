using Microsoft.AspNetCore.Mvc;
using MVCWithJQueryAJAX.Models;

namespace MVCWithJQueryAJAX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static int _totalCount = 0;
        public HomeController(ILogger<HomeController> logger)//depedancy injection
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("You called the index action at :"+DateTime.Now);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestJQuery()
        {
            return View();
        }
        //host://port/home/getcurrenttime
        public string GetCurrentTime() => DateTime.Now.ToString();

        public int GetCount() => _totalCount++;

        public IActionResult MakeOrder() => View();

        [HttpPost]
        public JsonResult MakeOrder(OrderModel order)
        {
            order.CalculatedResult = order.UnitPrice * order.Quantity;//300*3
            return Json(order);
        }

        public IActionResult Register()=>View();
        [HttpPost]
        public IActionResult Register(IList<EmployeeModel> employees)
        {
            ViewBag.RegisterCount=employees.Count;
            return View();
        }
    }
}
