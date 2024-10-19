using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
	[Table("ShiftAssign")]
	public class ShiftAssignEntity : BaseEntity
	{

		public string EmployeeId { get; set; }
		[ForeignKey(nameof(EmployeeId))]
		public EmployeeEntity Employee { get; set; }


		public string ShiftId { get; set; }
		[ForeignKey(nameof(ShiftId))]
		public ShiftEntity Shift { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
	}
}