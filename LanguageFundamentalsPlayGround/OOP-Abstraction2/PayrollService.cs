namespace OOP_Abstraction2
{
    public class PayrollService : IPayrollService
    {
        private decimal monthlySalaryAmount;
        public void CalculatePayroll(Staff staff, int WorkingDay, decimal benefit, decimal deduction)
        {
            decimal result=(staff.Salary/30*WorkingDay)+benefit-deduction;
            this.monthlySalaryAmount=result;
            Console.WriteLine("Monthly Pay Salary Amount : "+result);
        }

        public decimal TransfterMonthlySalary()
        {
           return monthlySalaryAmount;
        }
    }
}