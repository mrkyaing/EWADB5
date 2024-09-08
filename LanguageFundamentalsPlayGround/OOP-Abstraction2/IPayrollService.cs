namespace OOP_Abstraction2
{
    public interface IPayrollService
    {
        void CalculatePayroll(Staff staff, int WorkingDay, decimal benefit, decimal deduction);
        decimal TransfterMonthlySalary();
    }
}