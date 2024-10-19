namespace CloudHRMS.Models.ViewModels
{
	public class ShiftViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public TimeSpan InTime { get; set; }
		public TimeSpan OutTime { get; set; }
		public TimeSpan LateAfter { get; set; }
		public TimeSpan EarlyOutBefore { get; set; }
		public string AttendancePolicyId { get; set; }
		public string AttendancePolicyInfo { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}