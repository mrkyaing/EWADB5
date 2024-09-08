namespace OOP_Inheritance
{
    public class Animal
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int lifeSpan;
        public int LifeSpan
        {
            get { return lifeSpan; }
            set { lifeSpan = value; }
        }
        public void Eat() => Console.WriteLine($"{name} is eating.");
        public void Sleep() => Console.WriteLine($"{name} is sleeping.");
        public virtual void MakeSound() => Console.WriteLine($"{name} is making a sound.");
        public override string ToString() => $"Animal Information\nName:{name}\nType:{type}\nLifeSpan:{lifeSpan}";
    }
}