using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CloudHRMS.Controllers
{
	public class DailyAttendanceController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		public DailyAttendanceController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
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
				DailyAttendanceEntity dailyAttendanceEntity = new DailyAttendanceEntity()
				{
					Id = Guid.NewGuid().ToString(),
					AttendanceDate = dailyAttendanceViewModel.AttendanceDate,
					InTime = dailyAttendanceViewModel.InTime,
					OutTime = dailyAttendanceViewModel.OutTime,
					EmployeeId = dailyAttendanceViewModel.EmployeeId,
					DepartmentId = dailyAttendanceViewModel.DepartmentId,
					CreatedBy = "System"
				};
				_dbContext.DailyAttendances.Add(dailyAttendanceEntity);
				_dbContext.SaveChanges();
				ViewData["Info"] = "Successfully save the record to the system.";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{
				ViewData["Info"] = "Error occur when the record save to the system." + e.Message;
				ViewData["Status"] = false;
			}
			bindEmployeeData();
			bindDepartmentData();
			return View();
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
				ViewData["Info"] = "Successfully update the record to the system.";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{
				ViewData["Info"] = "Error occur when the record update to the system." + e.Message;
				ViewData["Status"] = false;
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
					TempData["Info"] = "Delete success when delete the record.";
				}
			}
			catch (Exception e)
			{
				TempData["Info"] = "Error occur when delete the record." + e.Message;
			}
			return RedirectToAction("List");
		}
	}
}