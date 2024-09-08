namespace OOP_Abstraction2
{
    public class Staff : IShowDetail
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public BankAccount bankAccount;//Has-A relationship between Staff and BankAccount 
        public Staff(BankAccount account)
        {
            bankAccount = account;
        }
        public void ShowDetail()
        {
            Console.WriteLine($"Name:{Name}");
            Console.WriteLine($"Position:{Position}");
            Console.WriteLine($"Address:{Address}");
            Console.WriteLine($"Salary:{Salary}");
            Console.WriteLine($"Department:{Department}");
        }
    }
}