using CloudHRMS.Models.ViewModels;
using CloudHRMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class PositionController : Controller
    {
        #region private variables
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

        #region Update Function
        public IActionResult Edit(string Id) => View(_positionService.GetById(Id));

        [HttpPost]
        public IActionResult Update(PositionViewModel positionViewModel)
        {
            try
            {
                _positionService.Update(positionViewModel);
                error.Message = "Successful update to system";
            }
            catch (Exception)
            {
                error.Message = "Error occur";
                error.IsOccurError = true;

            }
            TempData["Info"] = error;
            return RedirectToAction("List");
        }
        #endregion

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