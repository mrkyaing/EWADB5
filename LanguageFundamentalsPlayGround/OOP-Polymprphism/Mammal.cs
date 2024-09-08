namespace OOP_Polymprphism
{
    public class Mammal
    {
        public string Name { get; set; }
        public string EatType { get; set; }
        public int LifeSpan { get; set; }
        public void Eat() => Console.WriteLine($"{Name} is eating");
        public virtual void Sound() => Console.WriteLine("Speaking");
        public override string ToString() => $"Name:{Name}\nEatType:{EatType}\nLifeSpan:{LifeSpan}";
    }
}