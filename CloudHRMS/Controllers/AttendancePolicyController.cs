using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class AttendancePolicyController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public AttendancePolicyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize(Roles = "HR")]
        public IActionResult Entry() => View();

        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Entry(AttendancePolicyViewModel attendancePolicyViewModel)
        {
            try
            {
                AttendancePolicyEntity attendancePolicyEntity = new AttendancePolicyEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = attendancePolicyViewModel.Name,
                    NumberOfLateTime = attendancePolicyViewModel.NumberOfLateTime,
                    NumberOfEarlyOutTime = attendancePolicyViewModel.NumberOfEarlyOutTime,
                    DeductionInAmount = attendancePolicyViewModel.DeductionInAmount,
                    DeductionInDay = attendancePolicyViewModel.DeductionInDay,
                    CreatedBy = "System"
                };
                _dbContext.AttendancePolicies.Add(attendancePolicyEntity);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Successfully save the record to the system.";
                TempData["IsErrorOccur"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occur when the record save to the system." + e.Message;
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            IList<AttendancePolicyViewModel> attendancePolicy = _dbContext.AttendancePolicies.Select(s => new AttendancePolicyViewModel
            {
                Id = s.Id,
                Name = s.Name,
                NumberOfLateTime = s.NumberOfLateTime,
                NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                DeductionInAmount = s.DeductionInAmount,
                DeductionInDay = s.DeductionInDay
            }).ToList();
            return View(attendancePolicy);
        }
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id)
        {
            AttendancePolicyViewModel attendancePolicy = _dbContext.AttendancePolicies.Where(w => w.Id == id && w.IsActive).Select(s => new AttendancePolicyViewModel
            {
                Id = s.Id,
                Name = s.Name,
                NumberOfLateTime = s.NumberOfLateTime,
                NumberOfEarlyOutTime = s.NumberOfEarlyOutTime,
                DeductionInAmount = s.DeductionInAmount,
                DeductionInDay = s.DeductionInDay,
                CreatedOn = s.CreatedAt,
                UpdatedOn = s.UpdatedAt
            }).FirstOrDefault();
            return View(attendancePolicy);
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(AttendancePolicyViewModel attendancePolicyViewModel)
        {
            try
            {
                AttendancePolicyEntity attendancePolicyEntity = _dbContext.AttendancePolicies.Find(attendancePolicyViewModel.Id);
                if (attendancePolicyEntity is not null)
                {
                    attendancePolicyEntity.Name = attendancePolicyViewModel.Name;
                    attendancePolicyEntity.NumberOfEarlyOutTime = attendancePolicyViewModel.NumberOfEarlyOutTime;
                    attendancePolicyEntity.NumberOfLateTime = attendancePolicyViewModel.NumberOfLateTime;
                    attendancePolicyEntity.DeductionInAmount = attendancePolicyViewModel.DeductionInAmount;
                    attendancePolicyEntity.DeductionInDay = attendancePolicyViewModel.DeductionInDay;
                    attendancePolicyEntity.UpdatedAt = DateTime.Now;
                    attendancePolicyEntity.UpdatedBy = "System";
                    attendancePolicyEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                    _dbContext.AttendancePolicies.Update(attendancePolicyEntity);
                    _dbContext.SaveChanges();
                    TempData["Msg"] = "Successfully update the record to the system.";
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occur when the record update to the system." + e.Message;
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }

        [Authorize(Roles = "HR")]
        public IActionResult Delete(string id)
        {
            try
            {
                AttendancePolicyEntity attendancePolicy = _dbContext.AttendancePolicies.Find(id);
                if (attendancePolicy != null)
                {
                    attendancePolicy.IsActive = false;
                    _dbContext.AttendancePolicies.Update(attendancePolicy);
                    _dbContext.SaveChanges();
                    TempData["Msg"] = "Successfully update the record to the system.";
                    TempData["IsErrorOccur"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occur when the record delete from the system.";
                TempData["IsErrorOccur"] = true;
            }
            return RedirectToAction("List");
        }
    }
}