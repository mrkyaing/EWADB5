namespace CloudHRMS.Models.ViewModels
{
    public class AttendanceMasterCalculatedData
    {
        public string EmployeeId { get; set; }
        public decimal BasicPay { get; set; }
        public string DepartmentId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LateCount { get; set; }
        public int EarlyOutCount { get; set; }
        public int LeaveCount { get; set; }
        public int AttendanceDays { get; set; }
    }
}