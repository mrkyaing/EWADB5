namespace CloudHRMS.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }// to Edit/Update and Delete function
        public string No { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }//Date Of Birth
        public DateTime DOE { get; set; }//Date Of Employment
        public decimal Salary { get; set; }
        public DateTime? DOR { get; set; }//Date of Retirement
        public string Address { get; set; }
    }
}
