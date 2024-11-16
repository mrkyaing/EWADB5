using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [Authorize(Roles = "HR")]
        public IActionResult Entry()
        {
            return View();
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Entry(DepartmentViewModel departmentViewModel)
        {
            var isAlreadyExists = _applicationDbContext.Departments.Where(w => w.Code == departmentViewModel.Code).Any();
            if (isAlreadyExists)
            {
                TempData["Msg"] = $"This Code {departmentViewModel.Code} is already exists in the system.";
                return View(departmentViewModel);
            }
            try
            {
                DepartmentEntity departmentEntity = new DepartmentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = departmentViewModel.Code,
                    Description = departmentViewModel.Description,
                    ExtensionPhone = departmentViewModel.ExtensionPhone,
                    CreatedBy = "System"
                };
                _applicationDbContext.Departments.Add(departmentEntity);
                _applicationDbContext.SaveChanges();
                TempData["Msg"] = "Successfully created the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception ex)
            {

                TempData["Msg"] = "Oh,Error occur when create  the record to the system";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            IList<DepartmentViewModel> departments = _applicationDbContext.Departments
                                                        .Where(w => w.IsActive)
                                                        .Select(s => new DepartmentViewModel
                                                        {
                                                            Id = s.Id,
                                                            Code = s.Code,
                                                            Description = s.Description,
                                                            ExtensionPhone = s.ExtensionPhone,


                                                        }).ToList();
            return View(departments);
        }
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id)
        {
            if (id != null)
            {
                DepartmentViewModel organization = _applicationDbContext.Departments.Where(x => x.Id == id).Select(s => new DepartmentViewModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description,
                    ExtensionPhone = s.ExtensionPhone
                }).SingleOrDefault();
                return View(organization);
            }
            else
            {
                return RedirectToAction("List");
            }
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(DepartmentViewModel departmentViewModel)
        {
            try
            {
                DepartmentEntity departmentEntity = _applicationDbContext.Departments.Find(departmentViewModel.Id);
                {
                    departmentEntity.Code = departmentViewModel.Code;
                    departmentEntity.Description = departmentViewModel.Description;
                    departmentEntity.ExtensionPhone = departmentViewModel.ExtensionPhone;
                    departmentEntity.UpdatedBy = "System";
                    departmentEntity.UpdatedAt = DateTime.Now;
                    departmentEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                };
                _applicationDbContext.Departments.Update(departmentEntity);
                _applicationDbContext.SaveChanges();
                TempData["Msg"] = "Successfully Updated the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception ex)
            {

                TempData["Msg"] = "Oh,Error occurs when updating the record from the system.";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }

        [Authorize(Roles = "HR")]
        //port://host/employee/delete?id=10
        public IActionResult Delete(string Id)
        {
            try
            {
                var existingDepartment = _applicationDbContext.Departments.Where(w => w.Id == Id).SingleOrDefault();
                if (existingDepartment is not null)
                {
                    existingDepartment.IsActive = false;
                    _applicationDbContext.Update(existingDepartment);
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
    }
}