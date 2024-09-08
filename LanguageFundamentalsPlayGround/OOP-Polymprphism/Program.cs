namespace OOP_Polymprphism;

class Program
{
    static void Main(string[] args)
    {
        Cat myCat = new Cat()
        {
            Name = "juccy",
            EatType = "fired fish",
            LifeSpan = 1
        };
        Console.WriteLine(myCat);
        myCat.Eat();
        myCat.Sound();

        Programmer progremmer = new Programmer()
        {
            Name = "MG",
            EatType = "meal and vegetable",
            LifeSpan = 30,
            BloodType = "B",
            Phone = "09256235745",
            Address = "YGN",
            WorkType = ".NET Fullstack Developer",
        };
        Console.WriteLine(progremmer);
        progremmer.Eat();
        progremmer.Sound();

        Doctor doctor = new Doctor()
        {
            Name = "Dr.Sein",
            EatType = "meal and vegetable",
            LifeSpan = 30,
            BloodType = "B",
            Phone = "093265311",
            Address = "YGN",
            WorkType = "OG",
        };
        Console.WriteLine(doctor);
        doctor.Eat();
        doctor.Sound();
    }
}
