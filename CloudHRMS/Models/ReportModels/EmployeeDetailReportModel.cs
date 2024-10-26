using CloudHRMS.Models.ViewModels;

namespace CloudHRMS.Models.ReportModels
{
    public class EmployeeDetailReportModel
    {
        public string No { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public decimal Salary { get; set; }
        public DateTime? DOR { get; set; }
        public string Address { get; set; }
        public string DepartmentInfo { get; set; }
        public string PositonInfo { get; set; }

        public IList<EmployeeViewModel> Employees { get; set; }
        public IList<DepartmentViewModel> Departments { get; set; }
    }
}
