using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class PositionController : Controller
    {
        #region private variables
        private readonly IPositionService _positionService;
        #endregion

        #region constructor
        public PositionController(IPositionService positionService) => this._positionService = positionService;
        #endregion

        #region Create Function
        //[Authorize(Roles = "HR")]
        public IActionResult Entry() => View();
        // [Authorize(Roles = "HR")]

        [HttpPost]
        public IActionResult Entry(PositionViewModel positionViewModel)
        {
            if (ModelState.IsValid)
            {
                _positionService.Create(positionViewModel);
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion

        #region Retrieve Function
        public IActionResult List() => View(_positionService.ReteriveAll());
        #endregion

        [Authorize(Roles = "HR")]
        #region Update Function
        public IActionResult Edit(string Id) => View(_positionService.GetById(Id));
        [Authorize(Roles = "HR")]
        [HttpPost]
        public IActionResult Update(PositionViewModel positionViewModel)
        {
            try
            {
                _positionService.Update(positionViewModel);
                TempData["Msg"] = "Successfully Updated the record from the system";
                TempData["IsOccurError"] = false;
            }
            catch (Exception)
            {
                TempData["Msg"] = "Oh Error occur when Update the record from the system";
                TempData["IsOccurError"] = true;

            }
            return RedirectToAction("List");
        }
        #endregion
        [Authorize(Roles = "HR")]
        #region Delete Function
        [HttpPost]
        public IActionResult Delete(string Id)
        {
            try
            {
                _positionService.Delete(Id);
                // Return JSON response for success
                return Json(new { success = true, message = "Record deleted successfully." });
            }
            catch (Exception e)
            {
                // Return JSON response for failure
                return Json(new { success = false, message = "Error: " + e.Message });
            }

        }
        #endregion
    }
}