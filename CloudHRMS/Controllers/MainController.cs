using CloudHRMS.DAO;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class MainController : Controller
    {
        private readonly IUserService _userService;

        public MainController(IUserService userService)
        {
            _userService = userService;
        }

        public string UserId
        {
            get
            {
                string userId = "unknown";
                if (User.Identity is not null)
                {
                    var user = _userService.FindByUserName(User.Identity.Name).Result;
                    return user.Id;
                }
                return userId;
            }
        }
    }
}
