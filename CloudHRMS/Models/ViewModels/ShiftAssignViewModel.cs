namespace CloudHRMS.Models.ViewModels
{
	public class ShiftAssignViewModel
	{
		public string Id { get; set; }
		public string EmployeeId { get; set; }
		public string EmployeeInfo { get; set; }
		public string ShiftId { get; set; }
		public string ShiftInfo { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }

	}
}