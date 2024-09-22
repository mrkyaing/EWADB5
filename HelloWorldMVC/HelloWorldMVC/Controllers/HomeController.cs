using Microsoft.AspNetCore.Mvc;
namespace HelloWorldMVC.Controllers
{
    public class HomeController:Controller
    {
        //host:port/home/sayhello?name="MG"&country=Myanmar
        public string SayHello(string name,string country) => $"Hi,{name},Nice to see you!!,Are you from {country}?";
        //Index is action Method
        //host://port/home/index
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult Welcome()
        {
            int hour = DateTime.Now.Hour;
            string now = DateTime.Now.ToShortTimeString();
            ViewBag.Greeting = hour < 12 ? $"Hello,Good Morning:{now}": $"Hello,Good Afternoon :{now}";
            return View();
        }
        [HttpGet]
        //host://port/home/order
        public JsonResult Order()
        {
            var myOrder = new { orderId = "O001", orderName = "Shopping item", UnitPrice = 2000 };
            return Json(myOrder);
        }
        //you need to send the data to n1 and n2 parameters as QueryString
        //host://port/home/multiply?n1=10&n2=10
        public int Multiply(int n1,int n2)
        {
            return n1 * n2;
        }
        //[NonAction]
        public FileResult DownloadImage()
        {
            var fileName = "20yearsafterprogramming.png";
            var filePath = Path.Combine("wwwroot/MyFiles/", fileName);
            byte[] fileInByte=System.IO.File.ReadAllBytes(filePath);
            return File(fileInByte,"text/png",fileName);
        }
        [HttpGet]
        public int Add(int n1, int n2) => n1 + n2;//host://port/home/add  output : 2

        [HttpPost]
        public int Sum(int n1, int n2)//SayHello ..//host:/port/home/sayhello
        {
            return n1 + n2;
        }
        ////host://port/home/iam
        //[ActionName("iam")]
        public IActionResult DoMe()
        {
            ViewData["Me"] = "Bill";
            TempData["Msg"] = "Weekend";
            TempData.Keep();
            return View();
        }
        public IActionResult Test()
        {
            ViewData["V"] = ViewData["Me"];//NULL
            if (TempData.ContainsKey("Msg"))
            {
                var data = TempData["Msg"].ToString();
                ViewData["T"] = data;//value of TempData ,"Weekend"
            }
            return View();
        }
    }
}
