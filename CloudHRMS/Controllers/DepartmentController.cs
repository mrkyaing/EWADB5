using CloudHRMS.DAO;
using CloudHRMS.Models.Entities;
using CloudHRMS.Models.ViewModels;
using CloudHRMS.Utility.NetworkHelper;
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

		public IActionResult Entry()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Entry(DepartmentViewModel departmentViewModel)
		{
			var error = new ErrorViewModel();
			var isAlreadyExists = _applicationDbContext.Departments.Where(w => w.Code == departmentViewModel.Code).Any();
			if (isAlreadyExists)
			{
				error.Message = $"This Code {departmentViewModel.Code} is already exists in the system.";
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
				error.Message = "Transaction save successful to the System";
			}
			catch (Exception ex)
			{

				error.Message = "Transaction not save,found error";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;

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
		[HttpPost]
		public IActionResult Update(DepartmentViewModel departmentViewModel)
		{
			var error = new ErrorViewModel();
			var isAlreadyExists = _applicationDbContext.Departments.Where(w => w.Code == departmentViewModel.Code).Any();
			if (isAlreadyExists)
			{
				error.Message = $"This Code {departmentViewModel.Code} is already exists in the system.";
				return View("Edit", departmentViewModel);
			}
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
				_applicationDbContext.Departments.Add(departmentEntity);
				_applicationDbContext.SaveChanges();
				error.Message = "Transaction save successful to the System";
			}
			catch (Exception ex)
			{

				error.Message = "Transaction not save,found error";
				error.IsOccurError = true;
			}
			ViewBag.Msg = error;
			return RedirectToAction("List");
		}
	}
}