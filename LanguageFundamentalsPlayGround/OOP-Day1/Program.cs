namespace OOP_Day1;
using StudentInfo;
using TeacherInfo;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("OOP Practice Day");
        Console.WriteLine("My Car Information");
        Car myCar=new Car();//Craete a car object 
        myCar.color="Red";//write the value to the states of class 
        myCar.licenceNo="YGN-303";
        myCar.gearNo=1;
        myCar.PlayHorn(2);
        myCar.StartEngine();
        myCar.Drive();
        myCar.StopEngine();
        Console.WriteLine("My Car Color is "+myCar.color);//read the value from the state of car class
        Console.WriteLine("Your Car information.");
        Car yourCar=new Car();
        yourCar.gearNo=3;
        yourCar.StartEngine();
        Console.WriteLine("My New Car Information");
        Car newCar=new Car(100,3,"Black","MDY-503");
        newCar.Drive();
        Console.WriteLine("Method overloading demo");
        Cat myCat=new Cat();
        myCat.Sound();//Meow
        myCat.Sound(3);//Meow  Meow Meow 

        Utilitity.GetDate();
        Console.WriteLine(Utilitity.message);
        Car.GetModel();
        Teacher.Greeting();
        Student.Greeting();
        Student student=new Student();
        student.SayHello();
    }
}
