using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class EmployeeController : Controller
    {
        //declare the private global variable for ApplicationDbContext
        private readonly ApplicationDbContext _applicationDbContext;

        //Constructor Dependency injection in here to call the ApplicationDbContext
        public EmployeeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(EmployeeViewModel employeeViewModel)
        {
            var error = new ErrorViewModel();
            try
            {
                //DTO: data transfer object from View Models to Entity
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),//new GudID for  primary key :36
                    No = employeeViewModel.No,
                    FullName = employeeViewModel.FullName,
                    DOB = employeeViewModel.DOB,
                    DOE = employeeViewModel.DOE,
                    Phone = employeeViewModel.Phone,
                    Address = employeeViewModel.Address,
                    Salary = employeeViewModel.Salary,
                    Gender = employeeViewModel.Gender,
                    Email = employeeViewModel.Email,
                    DOR = employeeViewModel.DOR,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    IsActive = true,
                    IpAddress = GetIpAddressOfMachine()
                };
                _applicationDbContext.Employees.Add(employeeEntity);//adding the Entity to the context
                _applicationDbContext.SaveChanges();//actually save the connected database
                error.Message = "Successfully save the record to the system";
            }
            catch (Exception ex)
            {
                error.Message = "Oh,Error occur when saving the record to the system.";
                error.IsOccurError = true;
            }
            ViewBag.Msg = error;
            return View();
        }

        public string GetIpAddressOfMachine()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
