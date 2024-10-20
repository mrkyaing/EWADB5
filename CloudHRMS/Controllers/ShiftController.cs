using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class ShiftController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		public ShiftController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public IActionResult Entry()
		{
			bindAttendancePolicyData();
			return View();
		}

		private void bindAttendancePolicyData()
		{
			IList<AttendancePolicyViewModel> attendancePolicies = _dbContext.AttendancePolicies.Where(w => w.IsActive).Select(s =>
			new AttendancePolicyViewModel
			{
				Id = s.Id,
				Name = s.Name
			}).ToList();
			ViewBag.AttendancePolicies = attendancePolicies;
		}

		[HttpPost]
		public IActionResult Entry(ShiftViewModel shiftViewModel)
		{
			try
			{
				ShiftEntity shiftEntity = new ShiftEntity()
				{
					Id = Guid.NewGuid().ToString(),
					Name = shiftViewModel.Name,
					InTime = shiftViewModel.InTime,
					OutTime = shiftViewModel.OutTime,
					LateAfter = shiftViewModel.LateAfter,
					EarlyOutBefore = shiftViewModel.EarlyOutBefore,
					AttendancePolicyId = shiftViewModel.AttendancePolicyId,
					CreatedBy = "System"
				};
				_dbContext.Shifts.Add(shiftEntity);
				_dbContext.SaveChanges();
				ViewData["Info"] = "Successfully save the record to the system.";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{
				ViewData["Info"] = "Error occur when the record save to the system." + e.Message;
				ViewData["Status"] = false;
			}
			bindAttendancePolicyData();
			return View();
		}

		public IActionResult List()
		{
			IList<ShiftViewModel> shifts = (from s in _dbContext.Shifts
											join a in _dbContext.AttendancePolicies
											on s.AttendancePolicyId equals a.Id
											where s.IsActive && a.IsActive
											select new ShiftViewModel
											{
												Id = s.Id,
												Name = s.Name,
												InTime = s.InTime,
												OutTime = s.OutTime,
												LateAfter = s.LateAfter,
												EarlyOutBefore = s.EarlyOutBefore,
												AttendancePolicyInfo = a.Name,
											}).ToList();

			return View(shifts);
		}

		public IActionResult Edit(string id)
		{
			ShiftViewModel shiftView = _dbContext.Shifts.Where(w => w.Id == id && w.IsActive).Select(s => new ShiftViewModel
			{
				Id = s.Id,
				Name = s.Name,
				InTime = s.InTime,
				OutTime = s.OutTime,
				LateAfter = s.LateAfter,
				EarlyOutBefore = s.EarlyOutBefore,
				AttendancePolicyId = s.AttendancePolicyId,
				CreatedOn = s.CreatedAt,
				UpdatedOn = s.UpdatedAt
			}).FirstOrDefault();
			bindAttendancePolicyData();
			return View(shiftView);
		}

		[HttpPost]
		public IActionResult Update(ShiftViewModel shiftViewModel)
		{
			try
			{
				ShiftEntity shiftEntity = _dbContext.Shifts.Find(shiftViewModel.Id);
				shiftEntity.Name = shiftViewModel.Name;
				shiftEntity.InTime = shiftViewModel.InTime;
				shiftEntity.OutTime = shiftViewModel.OutTime;
				shiftEntity.LateAfter = shiftViewModel.LateAfter;
				shiftEntity.EarlyOutBefore = shiftViewModel.EarlyOutBefore;
				shiftEntity.AttendancePolicyId = shiftViewModel.AttendancePolicyId;
				shiftEntity.UpdatedAt = DateTime.Now;
				shiftEntity.UpdatedBy = "System";
				shiftEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
				_dbContext.Shifts.Update(shiftEntity);
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

		public IActionResult Delete(string id)
		{
			try
			{
				ShiftEntity shiftEntity = _dbContext.Shifts.Find(id);
				if (shiftEntity != null)
				{
					shiftEntity.IsActive = false;
					_dbContext.Shifts.Update(shiftEntity);
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