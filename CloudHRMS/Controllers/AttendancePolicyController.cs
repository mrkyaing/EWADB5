using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
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
		public IActionResult Entry() => View();

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
				ViewData["Info"] = "Successfully save the record to the system.";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{
				ViewData["Info"] = "Error occur when the record save to the system." + e.Message;
				ViewData["Status"] = false;
			}
			return View();
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
					ViewData["Info"] = "Successfully update the record to the system.";
					ViewData["Status"] = true;
				}
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
				AttendancePolicyEntity attendancePolicy = _dbContext.AttendancePolicies.Find(id);
				if (attendancePolicy != null)
				{
					attendancePolicy.IsActive = false;
					_dbContext.AttendancePolicies.Update(attendancePolicy);
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