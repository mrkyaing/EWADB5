using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using CloudHRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class EmployeeController : Controller
    {
        //declare the private global variable for ApplicationDbContext
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserService _userService;

        //Constructor Dependency injection in here to call the ApplicationDbContext
        public EmployeeController(ApplicationDbContext applicationDbContext, IUserService userService)
        {
            _applicationDbContext = applicationDbContext;
            _userService = userService;
        }
        [Authorize(Roles = "HR")]
        public IActionResult Entry()
        {
            var employeeViewModel = new EmployeeViewModel();

            employeeViewModel.DepartmentsViewModel = _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            employeeViewModel.PositionsViewModel = _applicationDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();

            return View(employeeViewModel);//passing the employee viewModel to the related View
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public async Task<IActionResult> Entry(EmployeeViewModel employeeViewModel)
        {
            try
            {

                string userId = await _userService.CreateUser(employeeViewModel.FullName, employeeViewModel.Email);
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["Msg"] = "Error occur when user creationg process.";
                    TempData["IsOccurError"] = false;
                    return RedirectToAction("List");
                }
                //DTO: data transfer object from View Models to Entity
                EmployeeEntity employeeEntity = new EmployeeEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    No = employeeViewModel.No,
                    FullName = employeeViewModel.FullName,
                    DOB = DateTime.SpecifyKind(employeeViewModel.DOB, DateTimeKind.Utc),
                    DOE = DateTime.SpecifyKind(employeeViewModel.DOE, DateTimeKind.Utc),
                    Phone = employeeViewModel.Phone,
                    Address = employeeViewModel.Address,
                    Salary = employeeViewModel.Salary,
                    Gender = employeeViewModel.Gender,
                    Email = employeeViewModel.Email,
                    DOR = employeeViewModel.DOR is not null ? DateTime.SpecifyKind((DateTime)employeeViewModel.DOR, DateTimeKind.Utc) : null,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    IsActive = true,
                    DepartmentId = employeeViewModel.DepartmentId,
                    PositionId = employeeViewModel.PositionId,
                    UserId = userId
                };
                _applicationDbContext.Employees.Add(employeeEntity);//adding the Entity to the context
                _applicationDbContext.SaveChanges();//actually save the connected database
                TempData["Msg"] = "Successfully created the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Error Occur created the record to the system";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            //DTO : Entity to View Model data exchange
            IList<EmployeeViewModel> employees = (from e in _applicationDbContext.Employees
                                                  join d in _applicationDbContext.Departments
                                                  on e.DepartmentId equals d.Id
                                                  join p in _applicationDbContext.Positions
                                                  on e.PositionId equals p.Id
                                                  where e.IsActive && p.IsActive && d.IsActive
                                                  select new EmployeeViewModel
                                                  {
                                                      Id = e.Id,
                                                      No = e.No,
                                                      FullName = e.FullName,
                                                      Email = e.Email,
                                                      Phone = e.Phone,
                                                      Address = e.Address,
                                                      Salary = e.Salary,
                                                      Gender = e.Gender,
                                                      DOB = e.DOB,
                                                      DOE = e.DOE,
                                                      DOR = e.DOR,
                                                      DepartmentId = e.DepartmentId,//CURD Process
                                                      PositionId = e.PositionId,//CURD Process
                                                      DepartmentInfo = d.Description,//to display UI List
                                                      PositonInfo = p.Description,//to display UI List
                                                      UserId = e.UserId
                                                  }).ToList();
            if (!User.IsInRole("HR"))
            {
                var loginedUser = _userService.FindByUserName(User.Identity.Name).Result;
                employees = employees.Where(u => u.UserId == loginedUser.Id).ToList();
            }
            return View(employees);//pass the viewModel object to the related view
        }
        [Authorize(Roles = "HR")]
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
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string Id)
        {

            var employeeViewModel = _applicationDbContext.Employees.Where(w => w.Id == Id).Select(s => new EmployeeViewModel
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
                DOR = s.DOR,
                DepartmentId = s.DepartmentId,
                PositionId = s.PositionId
            }).SingleOrDefault();
            employeeViewModel.DepartmentsViewModel = _applicationDbContext.Departments.Where(w => w.IsActive).Select(s => new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            employeeViewModel.PositionsViewModel = _applicationDbContext.Positions.Where(w => w.IsActive).Select(s => new PositionViewModel
            {
                Id = s.Id,
                Code = s.Code
            }).ToList();
            return View(employeeViewModel);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(EmployeeViewModel employeeViewModel)
        {
            try
            {
                //DTO: data transfer object from View Models to Entity
                var existingEmployeeEntity = _applicationDbContext.Employees.Find(employeeViewModel.Id);
                existingEmployeeEntity.No = employeeViewModel.No;
                existingEmployeeEntity.FullName = employeeViewModel.FullName;
                existingEmployeeEntity.DOB = employeeViewModel.DOB;
                existingEmployeeEntity.DOE = employeeViewModel.DOE;
                existingEmployeeEntity.Phone = employeeViewModel.Phone;
                existingEmployeeEntity.Address = employeeViewModel.Address;
                existingEmployeeEntity.Salary = employeeViewModel.Salary;
                existingEmployeeEntity.Gender = employeeViewModel.Gender;
                existingEmployeeEntity.Email = employeeViewModel.Email;
                existingEmployeeEntity.DOR = employeeViewModel.DOR;
                existingEmployeeEntity.UpdatedAt = DateTime.Now;
                existingEmployeeEntity.UpdatedBy = "System";
                existingEmployeeEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                existingEmployeeEntity.DepartmentId = employeeViewModel.DepartmentId;
                existingEmployeeEntity.PositionId = employeeViewModel.PositionId;
                _applicationDbContext.Employees.Update(existingEmployeeEntity);//adding the Entity to the context
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
    }
}
