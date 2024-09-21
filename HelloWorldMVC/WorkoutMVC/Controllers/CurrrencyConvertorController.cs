using Microsoft.AspNetCore.Mvc;

namespace WorkoutMVC.Controllers
{
    public class CurrrencyConvertorController : Controller
    {
        public IActionResult ConvertorV1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConvertorV1(string fromCurrency,decimal amount)
        {
            if(string.IsNullOrEmpty(fromCurrency))
            {
                ViewData["Invalid"] = "Please select currency";
                return View();
            }
            decimal result = 0;
            switch (fromCurrency)
            {
                case "USD":result = amount * 5000;break;
                case "SDG": result = amount * 3000; break;
                case "YAN": result = amount * 750; break;
                case "BAHT": result = amount * 150; break;
            }
            ViewBag.Result=result;
            return View();
        }


        public IActionResult ConvertorV2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConvertorV2(string fromCurrency, decimal amount)
        {
            if (string.IsNullOrEmpty(fromCurrency))
            {
                ViewData["Invalid"] = "Please select currency";
                return View();
            }
            decimal result = 0;
            switch (fromCurrency)
            {
                case "USD": result = amount * 5000; break;
                case "SDG": result = amount * 3000; break;
                case "YAN": result = amount * 750; break;
                case "BAHT": result = amount * 150; break;
            }
            ViewBag.SelectedCurrency=fromCurrency;
            ViewBag.InputAmount = amount;
            ViewBag.Result = result;
            return View();
        }
    }
}
