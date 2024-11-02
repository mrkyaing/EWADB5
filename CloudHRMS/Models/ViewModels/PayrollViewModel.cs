namespace CloudHRMS.Models.ViewModels
{
    public class PayrollViewModel
    {
        public string Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public string EmployeeInfo { get; set; }
        public decimal BasicSalary { get; set; }
        public string DepartmentInfo { get; set; }
        public decimal AttendanceDays { get; set; }
        public decimal AttendanceDeduction { get; set; }
    }
}