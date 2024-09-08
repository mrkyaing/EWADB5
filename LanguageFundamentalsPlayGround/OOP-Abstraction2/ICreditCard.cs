namespace OOP_Abstraction2
{
    public interface ICreditCard
    {
        void GetUSDExchangeRate(decimal amount);
        void GetSDGExchangeRate(decimal amount);
        void GetTHAIExchangeRate(decimal amount);
        void GetJAPANYANExchangeRate(decimal amount);
    }
}