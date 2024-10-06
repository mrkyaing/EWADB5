using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CloudHRMS.Controllers
{
    public class EmployeeController : Controller
    {
        //declare the private global variable for ApplicationDbContext
        private readonly ApplicationDbContext _applicationDbContext;
        ErrorViewModel error = new ErrorViewModel();

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

        public IActionResult List()
        {
            //DTO : Entity to View Model data exchange
            IList<EmployeeViewModel> employees = _applicationDbContext.Employees.Where(w => w.IsActive).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                No = s.No,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Salary = s.Salary,
                Gender = s.Gender,
                DOB = s.DOB,
                DOE = s.DOE,
                DOR = s.DOR
            }).ToList();

            return View(employees);//pass the viewModel object to the related view
        }

        //port://host/employee/delete?id=10
        public IActionResult Delete(string Id)
        {
            try
            {
                var existingEmployee = _applicationDbContext.Employees.Where(w => w.Id == Id).SingleOrDefault();
                if (existingEmployee is not null)
                {
                    existingEmployee.IsActive = false;
                    _applicationDbContext.Update(existingEmployee);
                    _applicationDbContext.SaveChanges();
                    TempData["Msg"] = "Successfully Deleted the record from the system";
                    TempData["IsOccurError"] = false;
                }
            }
            catch (Exception e)
            {

                TempData["Msg"] = "Oh,Error occurs when deleting the record from the system.";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(string Id)
        {

            var employee = _applicationDbContext.Employees.Where(w => w.Id == Id).Select(s => new EmployeeViewModel
            {
                Id = s.Id,
                No = s.No,
                FullName = s.FullName,
                Email = s.Email,
                Phone = s.Phone,
                Address = s.Address,
                Salary = s.Salary,
                Gender = s.Gender,
                DOB = s.DOB,
                DOE = s.DOE,
                DOR = s.DOR
            }).SingleOrDefault();

            return View(employee);
        }
        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                //DTO: data transfer object from View Models to Entity
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = employeeViewModel.Id,
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
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "System",
                    IpAddress = GetIpAddressOfMachine()
                };
                _applicationDbContext.Employees.Update(employeeEntity);//adding the Entity to the context
                _applicationDbContext.SaveChanges();//actually update the connected database
                TempData["Msg"] = "Successfully updated the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Error occurs when update the record to  the system";
                TempData["IsOccurError"] = true;
            }

            return RedirectToAction("List");
        }

        public string GetIpAddressOfMachine()
        {
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
