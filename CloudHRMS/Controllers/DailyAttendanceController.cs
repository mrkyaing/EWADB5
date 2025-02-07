﻿using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using CloudHRMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CloudHRMS.Controllers
{
    public class DailyAttendanceController : MainController
    {
        private readonly ApplicationDbContext _dbContext;
        public DailyAttendanceController(ApplicationDbContext dbContext, IUserService userService) : base(userService)
        {
            this._dbContext = dbContext;
        }

        [Authorize(Roles = "HR")]
        public IActionResult Entry()
        {
            bindEmployeeData();
            bindDepartmentData();
            return View();
        }

        private void bindDepartmentData()
        {
            IList<DepartmentViewModel> departments = _dbContext.Departments.Where(w => w.IsActive).Select(s =>
            new DepartmentViewModel
            {
                Id = s.Id,
                Code = s.Code + "/" + s.Description
            }).ToList();
            ViewBag.Departments = departments;
        }

        private void bindEmployeeData()
        {
            IList<EmployeeViewModel> positions = _dbContext.Employees.Where(w => w.IsActive).Select(s =>
             new EmployeeViewModel
             {
                 Id = s.Id,
                 No = s.No + "/" + s.FullName
             }).ToList();
            ViewBag.Employees = positions;
        }
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Entry(DailyAttendanceViewModel dailyAttendanceViewModel)
        {
            try
            {
                IList<DailyAttendanceEntity> dailyAttendances = new List<DailyAttendanceEntity>();

                if (dailyAttendanceViewModel.DepartmentId.Equals("[Select Department]"))
                {
                    var departmentId = _dbContext.Employees.Find(dailyAttendanceViewModel.EmployeeId).DepartmentId;
                    var existingDailyAttendances = _dbContext.DailyAttendances.Where(w => w.AttendanceDate.Date == dailyAttendanceViewModel.AttendanceDate.Date && w.DepartmentId == departmentId).ToList();
                    DeleteDailyAttendancesBeforeAdd(existingDailyAttendances);
                    if (dailyAttendanceViewModel.IsToCurrentDate)
                    {
                        var startDate = dailyAttendanceViewModel.AttendanceDate;
                        var now = DateTime.Now;
                        while (startDate.Date <= now.Date)
                        {
                            DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                AttendanceDate = startDate,
                                InTime = dailyAttendanceViewModel.InTime,
                                OutTime = dailyAttendanceViewModel.OutTime,
                                EmployeeId = dailyAttendanceViewModel.EmployeeId,
                                DepartmentId = departmentId,
                                CreatedBy = UserId
                            };
                            dailyAttendances.Add(dailyAttendanceEntity);
                            startDate = startDate.AddDays(1);
                        }
                    }
                    else
                    {
                        DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            AttendanceDate = dailyAttendanceViewModel.AttendanceDate,
                            InTime = dailyAttendanceViewModel.InTime,
                            OutTime = dailyAttendanceViewModel.OutTime,
                            EmployeeId = dailyAttendanceViewModel.EmployeeId,
                            DepartmentId = departmentId,
                            CreatedBy = UserId
                        };
                        dailyAttendances.Add(dailyAttendanceEntity);
                    }
                }
                else
                {
                    var employees = _dbContext.Employees.Where(w => w.DepartmentId == dailyAttendanceViewModel.DepartmentId).ToList();
                    var existingDailyAttendances = _dbContext.DailyAttendances.Where(w => w.AttendanceDate.Date.ToUniversalTime() == dailyAttendanceViewModel.AttendanceDate.Date.ToUniversalTime() && w.DepartmentId == dailyAttendanceViewModel.DepartmentId).ToList();
                    DeleteDailyAttendancesBeforeAdd(existingDailyAttendances);
                    if (dailyAttendanceViewModel.IsToCurrentDate)
                    {
                        var startDate = dailyAttendanceViewModel.AttendanceDate.ToUniversalTime();
                        var now = DateTime.Now;
                        while (startDate.Date <= now.Date)
                        {
                            foreach (var employee in employees)
                            {
                                DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    AttendanceDate = startDate.ToUniversalTime(),
                                    InTime = dailyAttendanceViewModel.InTime,
                                    OutTime = dailyAttendanceViewModel.OutTime,
                                    EmployeeId = employee.Id,
                                    DepartmentId = employee.DepartmentId,
                                    CreatedBy = UserId
                                };
                                dailyAttendances.Add(dailyAttendanceEntity);
                            }//end of for
                            startDate = startDate.AddDays(1);
                        }
                    }
                    else
                    {
                        foreach (var employee in employees)
                        {
                            DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                AttendanceDate = dailyAttendanceViewModel.AttendanceDate,
                                InTime = dailyAttendanceViewModel.InTime,
                                OutTime = dailyAttendanceViewModel.OutTime,
                                EmployeeId = employee.Id,
                                DepartmentId = employee.DepartmentId,
                                CreatedBy = UserId
                            };
                            dailyAttendances.Add(dailyAttendanceEntity);
                        }//end of for
                    }
                }

                _dbContext.DailyAttendances.AddRange(dailyAttendances);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Successfully created the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error occur when created the record to the system";
                TempData["IsOccurError"] = true;
            }
            bindEmployeeData();
            bindDepartmentData();
            return RedirectToAction("List");
        }

        private void DeleteDailyAttendancesBeforeAdd(List<DailyAttendanceEntity> existingDailyAttendances)
        {
            _dbContext.DailyAttendances.RemoveRange(existingDailyAttendances);
            _dbContext.SaveChanges();
        }

        public IActionResult List()
        {
            IList<DailyAttendanceViewModel> dailyAttendances = (from da in _dbContext.DailyAttendances
                                                                join d in _dbContext.Departments
                                                                on da.DepartmentId equals d.Id
                                                                join e in _dbContext.Employees
                                                                on da.EmployeeId equals e.Id
                                                                where da.IsActive && d.IsActive && e.IsActive
                                                                select new DailyAttendanceViewModel
                                                                {
                                                                    Id = da.Id,
                                                                    AttendanceDate = da.AttendanceDate,
                                                                    InTime = da.InTime,
                                                                    OutTime = da.OutTime,
                                                                    EmployeeInfo = e.No + "/" + e.FullName,
                                                                    DepartmentInfo = d.Code + "/" + d.Description
                                                                }).ToList();

            return View(dailyAttendances);
        }
        [Authorize(Roles = "HR")]
        public IActionResult Edit(string id)
        {
            DailyAttendanceViewModel dailyAttendanceView = _dbContext.DailyAttendances.Where(w => w.Id == id && w.IsActive).Select(s => new DailyAttendanceViewModel
            {
                Id = s.Id,
                AttendanceDate = s.AttendanceDate,
                InTime = s.InTime,
                OutTime = s.OutTime,
                DepartmentId = s.DepartmentId,
                EmployeeId = s.EmployeeId,
                CreatedOn = s.CreatedAt,
                UpdatedOn = s.UpdatedAt
            }).FirstOrDefault();
            bindDepartmentData();
            bindEmployeeData();
            return View(dailyAttendanceView);
        }

        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(DailyAttendanceViewModel dailyAttendanceViewModel)
        {
            try
            {
                DailyAttendanceEntity dailyAttendanceEntity = _dbContext.DailyAttendances.Find(dailyAttendanceViewModel.Id);

                dailyAttendanceEntity.AttendanceDate = dailyAttendanceViewModel.AttendanceDate;
                dailyAttendanceEntity.InTime = dailyAttendanceViewModel.InTime;
                dailyAttendanceEntity.OutTime = dailyAttendanceViewModel.OutTime;
                dailyAttendanceEntity.EmployeeId = dailyAttendanceViewModel.EmployeeId;
                dailyAttendanceEntity.DepartmentId = dailyAttendanceViewModel.DepartmentId;
                dailyAttendanceEntity.UpdatedAt = DateTime.Now;
                dailyAttendanceEntity.UpdatedBy = "System";
                dailyAttendanceEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
                _dbContext.DailyAttendances.Update(dailyAttendanceEntity);
                _dbContext.SaveChanges();
                TempData["Msg"] = "Successfully updated the record to the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Error  update the record to the system";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }


        [Authorize(Roles = "HR")]
        public IActionResult Delete(string id)
        {
            try
            {
                DailyAttendanceEntity dailyAttendanceEntity = _dbContext.DailyAttendances.Find(id);
                if (dailyAttendanceEntity != null)
                {
                    dailyAttendanceEntity.IsActive = false;
                    _dbContext.DailyAttendances.Update(dailyAttendanceEntity);
                    _dbContext.SaveChanges();
                    TempData["Msg"] = "Successfully deleted the record to the system";
                    TempData["IsOccurError"] = false;
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Oh,Error occur when it delete the record to the system";
                TempData["IsOccurError"] = true;
            }
            return RedirectToAction("List");
        }
    }
}