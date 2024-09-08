namespace OOP_Abstraction2;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Payroll System");
        BankAccount bankAccount = new BankAccount()
        {
            Branch = "YGN branch",
            OpeningBalance = 10000,
            AccountNo = "0000000001"
        };
        bankAccount.ShowDetail();
        Staff staff = new Staff(bankAccount)
        {
            Name = "SU SU",
            Position = "UI/UX Designer",
            Address = "YGN",
            Salary = 400000,
            Department = "IT"
        };
        staff.ShowDetail();
        IPayrollService payrollService = new PayrollService();
        payrollService.CalculatePayroll(staff, 30, 30000, 0);
        //Transfter Salary 
        bankAccount.Deposite(payrollService.TransfterMonthlySalary());
        bankAccount.CheckBalance();//400000+10000
        bankAccount.Withdraw(10000);//
        bankAccount.CheckBalance();//400000
        bankAccount.Withdraw(100000);//300000
    }
}
