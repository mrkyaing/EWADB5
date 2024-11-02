using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("Payroll")]
    public class PayrollEntity : BaseEntity
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeEntity Employee { get; set; }
        public string DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public DepartmentEntity Department { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal AttendanceDays { get; set; }
        public decimal AttendanceDeduction { get; set; }
    }
}