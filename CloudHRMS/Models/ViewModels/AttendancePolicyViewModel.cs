namespace CloudHRMS.Models.ViewModels
{
	public class AttendancePolicyViewModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int NumberOfLateTime { get; set; }
		public int NumberOfEarlyOutTime { get; set; }
		public decimal DeductionInAmount { get; set; }
		public int DeductionInDay { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
	}
}