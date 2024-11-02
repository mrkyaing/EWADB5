namespace CloudHRMS.Models.ViewModels
{
    public class PayrollProcessViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
    }
}