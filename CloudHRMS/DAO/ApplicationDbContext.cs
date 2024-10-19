using CloudHRMS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudHRMS.DAO
{
	public class ApplicationDbContext : DbContext
	{
		//Constructor changing to pass object to the parent's class constructor
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		//Define the DbSet Property what we want to use for our System Domains
		public DbSet<EmployeeEntity> Employees { get; set; }
		public DbSet<PositionEntity> Positions { get; set; }
		public DbSet<DepartmentEntity> Departments { get; set; }
		public DbSet<DailyAttendanceEntity> DailyAttendances { get; set; }
		public DbSet<ShiftEntity> Shifts { get; set; }
		public DbSet<AttendancePolicyEntity> AttendancePolicies { get; set; }
		public DbSet<ShiftAssignEntity> ShiftAssigns { get; set; }
	}
}
