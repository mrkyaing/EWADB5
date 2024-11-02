using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
    [Table("AttendanceMaster")]
    public class AttendanceMasterEntity : BaseEntity
    {
        public DateTime AttendanceDate { get; set; }
        public TimeSpan InTime { get; set; }
        public TimeSpan OutTime { get; set; }
        //Has-A Relationship between Employee and AttendanceMaster
        public string EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public virtual EmployeeEntity Employee { get; set; }

        //Has-A Relationship between Employee and Department
        public string DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentEntity Department { get; set; }

        //Has-A Relationship between Employee and Shift
        public string ShiftId { get; set; }
        [ForeignKey(nameof(ShiftId))]
        public virtual ShiftEntity Shift { get; set; }
        public bool IsLate { get; set; }
        public bool IsEarlyOut { get; set; }
        public bool IsLeave { get; set; }
    }
}