using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
	[Table("DailyAttendance")]
	public class DailyAttendanceEntity : BaseEntity
	{
		public DateTime AttendanceDate { get; set; }
		public TimeSpan InTime { get; set; }
		public TimeSpan OutTime { get; set; }
		public string EmployeeId { get; set; }
		[ForeignKey(nameof(EmployeeId))]
		public EmployeeEntity Employee { get; set; }

		public string DepartmentId { get; set; }
		[ForeignKey(nameof(DepartmentId))]
		public DepartmentEntity Department { get; set; }
	}
}