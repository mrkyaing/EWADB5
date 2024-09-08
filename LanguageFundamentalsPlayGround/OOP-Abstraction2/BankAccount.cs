namespace OOP_Abstraction2
{
    public class BankAccount:IShowDetail,ICreditCard
    {
        public string AccountNo { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Branch { get; set; }

        public void Deposite(decimal amount)
        {
            if (amount > 0)
            {
                OpeningBalance += amount;
            }
        }
        public void Withdraw(decimal amount)//100000
        {
            if (amount <= 0)
                throw new Exception("Invalid Withdraw");
            if (amount > OpeningBalance)
                throw new Exception("insufficent balance");
            OpeningBalance -= amount;
        }
        public void CheckBalance(){
            Console.WriteLine("Check balance "+OpeningBalance);
        }

        public void ShowDetail()
        {
            Console.WriteLine($"AccountNo:{AccountNo}");
            Console.WriteLine($"Branch:{Branch}");
            Console.WriteLine($"Opening balance:{OpeningBalance}");
        }

        public void GetUSDExchangeRate(decimal amount)
        {
            Console.WriteLine("USD Exchange rate:"+amount*3000);
        }

        public void GetSDGExchangeRate(decimal amount)
        {
            Console.WriteLine("Singapore SDG Exchange rate:"+amount*1500);
        }

        public void GetTHAIExchangeRate(decimal amount)
        {
            Console.WriteLine("Thai BAHT Exchange rate:"+amount*150);
        }

        public void GetJAPANYANExchangeRate(decimal amount)
        {
            Console.WriteLine("JAPAN YAN Exchange rate:"+amount*300);
        }
    }
}