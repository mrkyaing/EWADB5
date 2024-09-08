namespace OOP_Inheritance;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Animal animal = new Animal();
        animal.Name = "jacky";
        animal.Type = "Dog";
        animal.LifeSpan = 1;
        Console.WriteLine(animal);
        Dog myDog = new Dog();
        myDog.Name = "Juccy";
        myDog.Type = "Puckbull";
        myDog.LifeSpan = 1;
        Console.WriteLine(myDog);
        myDog.Eat();
        myDog.Sleep();
        myDog.MakeSound();//Woak
        Cat myCat = new Cat();
        myCat.Name = "bikicky";
        myCat.Type = "pet";
        myCat.LifeSpan = 1;
        myCat.SayHello();
        Console.WriteLine(myCat);
        myCat.Sleep();
        myCat.Eat();
        myCat.MakeSound();//Menow
    }
}
