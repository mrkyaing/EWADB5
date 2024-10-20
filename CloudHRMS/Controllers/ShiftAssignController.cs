using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class ShiftAssignController : Controller
	{
		private readonly ApplicationDbContext _dbContext;
		public ShiftAssignController(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Entry()
		{
			bindEmployeeData();
			bindShiftData();

			return View();
		}
		private void bindEmployeeData()
		{
			IList<EmployeeViewModel> employees = _dbContext.Employees.Where(w => w.IsActive).Select(s =>
			 new EmployeeViewModel
			 {
				 Id = s.Id,
				 FullName = s.FullName,
			 }).ToList();
			ViewBag.Employees = employees;     //passing the all of Departments to the UI
		}

		private void bindShiftData()
		{
			IList<ShiftViewModel> shifts = _dbContext.Shifts.Where(w => w.IsActive).Select(s =>
		   new ShiftViewModel
		   {
			   Id = s.Id,
			   Name = s.Name,
		   }).ToList();
			ViewBag.Shifts = shifts;    //passing the all of position to the UI
		}

		[HttpPost]
		public IActionResult Entry(ShiftAssignViewModel ui)
		{
			try
			{

				ShiftAssignEntity shiftAssignEntity = new ShiftAssignEntity()
				{
					Id = Guid.NewGuid().ToString(),
					EmployeeId = ui.EmployeeId,
					ShiftId = ui.ShiftId,
					FromDate = ui.FromDate,
					ToDate = ui.ToDate,
					CreatedBy = "System"

				};
				_dbContext.ShiftAssigns.Add(shiftAssignEntity);
				_dbContext.SaveChanges();
				ViewData["Info"] = "Successfully save the record to the system";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{

				ViewData["Info"] = "Error occur when the record save to the system" + e.Message;
				ViewData["Status"] = false;
			}
			bindEmployeeData();
			bindShiftData();
			return View();
		}
		public IActionResult List()
		{
			IList<ShiftAssignViewModel> shiftAssigns = (from s in _dbContext.ShiftAssigns
														join e in _dbContext.Employees
														on s.EmployeeId equals e.Id
														join t in _dbContext.Shifts
														on s.ShiftId equals t.Id
														where s.IsActive && e.IsActive && t.IsActive
														select new ShiftAssignViewModel

														{
															Id = s.Id,
															EmployeeInfo = e.FullName,
															ShiftInfo = t.Name,
															FromDate = s.FromDate,
															ToDate = s.ToDate,
														}).ToList();
			return View(shiftAssigns);
		}

		public IActionResult Edit(string id)

		{
			ShiftAssignViewModel shiftAssignView = _dbContext.ShiftAssigns.Where(w => w.Id == id && w.IsActive).Select(s => new ShiftAssignViewModel
			{
				Id = s.Id,
				EmployeeId = s.EmployeeId,
				ShiftId = s.ShiftId,
				FromDate = s.FromDate,
				ToDate = s.ToDate,

			}).FirstOrDefault();

			bindEmployeeData();
			bindShiftData();
			return View(shiftAssignView);
		}

		[HttpPost]
		public IActionResult Update(ShiftAssignViewModel ui)
		{
			try
			{
				ShiftAssignEntity shiftAssignEntity = _dbContext.ShiftAssigns.Find(ui.Id);
				{
					shiftAssignEntity.EmployeeId = ui.EmployeeId;
					shiftAssignEntity.ShiftId = ui.ShiftId;
					shiftAssignEntity.FromDate = ui.FromDate;
					shiftAssignEntity.ToDate = ui.ToDate;
					shiftAssignEntity.UpdatedBy = "System";
					shiftAssignEntity.UpdatedAt = DateTime.Now;
					shiftAssignEntity.IpAddress = NetworkHelper.GetMachinePublicIP();
				};
				_dbContext.ShiftAssigns.Update(shiftAssignEntity);
				_dbContext.SaveChanges();
				ViewData["Info"] = "Successfully update the record to the system";
				ViewData["Status"] = true;
			}
			catch (Exception e)
			{

				ViewData["Info"] = "Error occur when the record update to the system" + e.Message;
				ViewData["Status"] = false;
			}
			return RedirectToAction("List");
		}

		public IActionResult Delete(string id)
		{
			try
			{
				ShiftAssignEntity shiftAssignEntity = _dbContext.ShiftAssigns.Find(id);
				if (shiftAssignEntity != null)
				{
					shiftAssignEntity.IsActive = false; ;
					_dbContext.ShiftAssigns.Update(shiftAssignEntity);
					_dbContext.SaveChanges();
					TempData["info"] = "Delete Success when delete the record";
				}
			}
			catch (Exception)
			{

				TempData["info"] = "Error Occur when delete the record";
			}
			return RedirectToAction("List");
		}
	}
}