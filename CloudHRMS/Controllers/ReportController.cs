using CloudHRMS.DAO;
using CloudHRMS.Models.ReportModels;
using CloudHRMS.Services;
using CloudHRMS.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CloudHRMS.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportingService _reportingService;

        public ReportController(IReportingService reportingService)
        {
            this._reportingService = reportingService;
        }
        public IActionResult EmployeeDetailReport()
        {

            return View(GetBindData());
        }

        [HttpPost]
        public IActionResult EmployeeDetailReport(string departmentId, string fromEmployeeNo, string toEmployeeNo)
        {
            var employeeDetails = _reportingService.GetEmployeeDetailReport(fromEmployeeNo, toEmployeeNo, departmentId);
            string reportFileName = $"Employee Detail Report{DateTime.Now.ToString()}.xlsx";
            if (employeeDetails.Count > 0)
            {
                var fileContentsInBytes = ReportHelper.ExportToExcel(employeeDetails, reportFileName);
                var contentType = "application/vnd.openxmlformat-officedocument.spreedsheet.sheet";
                ViewData["Info"] = "Successfully export to excel.";
                ViewData["IsErrorOccur"] = true;
                return File(fileContentsInBytes, contentType, reportFileName);
            }
            else
            {
                ViewData["Info"] = "There is no data ,So We can't export excel.";
                ViewData["IsErrorOccur"] = true;
            }
            return View(GetBindData());
        }

        private EmployeeDetailReportModel GetBindData()
        {
            EmployeeDetailReportModel employeeDetailReportModel = new EmployeeDetailReportModel();
            employeeDetailReportModel.Employees = _reportingService.GetAllEmployees();
            employeeDetailReportModel.Departments = _reportingService.GetAllDepartments();
            return employeeDetailReportModel;
        }
    }
}
