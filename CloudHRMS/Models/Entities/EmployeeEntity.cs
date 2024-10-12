using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudHRMS.Models.Entities
{
	[Table("Employee")]
	public class EmployeeEntity : BaseEntity
	{
		public string No { get; set; }
		public string FullName { get; set; }
		[MaxLength(1)]
		public string Gender { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime DOB { get; set; }//Date Of Birth
		public DateTime DOE { get; set; }//Date Of Employment
		[Precision(18, 2)]
		public decimal Salary { get; set; }
		public DateTime? DOR { get; set; }//Date of Retirement
		public string Address { get; set; }

		public string DepartmentId { get; set; }//foreign Key
		[ForeignKey(nameof(DepartmentId))]
		public DepartmentEntity Department { get; set; }
		public string PositionId { get; set; }//foreign Key
		[ForeignKey(nameof(PositionId))]
		public PositionEntity Position { get; set; }
	}
}
