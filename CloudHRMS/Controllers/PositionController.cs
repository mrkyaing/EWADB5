using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
	public class PositionController : Controller
	{
		#region private Varialbes
		private readonly IPositionService _positionService;
		private ErrorViewModel error = new ErrorViewModel();
		#endregion

		#region constructor
		public PositionController(IPositionService positionService) => this._positionService = positionService;
		#endregion

		#region Create Function
		public IActionResult Entry() => View();


		[HttpPost]
		public IActionResult Entry(PositionViewModel positionViewModel)
		{
			try
			{
				_positionService.Create(positionViewModel);
				error.Message = "Successful save the record to the system ";
			}
			catch (Exception ex)
			{
				error.Message = "Error Occur";
				error.IsOccurError = true;
			}
			ViewBag.Message = error;
			return View();
		}
		#endregion

		#region Reterive Function
		public IActionResult List() => View(_positionService.ReteriveAll());
		#endregion

		#region Update Function
		public IActionResult Edit(string Id) => View(_positionService.GetById(Id));

		[HttpPost]
		public IActionResult Update(PositionViewModel positionViewModel)
		{
			try
			{
				_positionService.Update(positionViewModel);
				TempData["Msg"] = "Successful update to sytem";
				TempData["IsOccourError"] = false;
			}
			catch (Exception)
			{
				TempData["Msg"] = "Error Occour";
				TempData["Msg"] = true;

			}
			return RedirectToAction("List");
		}
		#endregion

		#region Delete Function
		public IActionResult Delete(string Id)
		{
			try
			{
				_positionService.Delete(Id);
				TempData["Msg"] = "Successful Delete from System";
				TempData["IsOccourError"] = false;
				// Return JSON response for success
				return Json(new { success = true, message = "Record deleted successfully." });
			}
			catch (Exception e)
			{

				TempData["Msg"] = "Error Occour";
				TempData["IsOccourError"] = true;
				// Return JSON response for failure
				return Json(new { success = false, message = "Error: " + e.Message });
			}

		}
		#endregion
	}
}