using CloudHRMS.Models.ReportModels;
using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Services
{
    public interface IReportingService
    {
        IList<EmployeeViewModel> GetAllEmployees();
        IList<DepartmentViewModel> GetAllDepartments();
        IList<EmployeeDetailReportModel> GetEmployeeDetailReport(string fromNo, string toNo, string Department);
    }
}
