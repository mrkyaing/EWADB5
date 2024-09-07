namespace OOP_Day1
{
    class Car
    {
        //states/Members of Car  
       public int speed, gearNo;
       public string color, licenceNo;
       //default constructor
       public Car(){

       }
       //constructor with 4 paremeters
       public Car(int speed,int gearno,string color,string licneceNo){
        this.speed=speed;
        this.gearNo=gearno;
        this.color=color;
        this.licenceNo=licneceNo;
       }
        //behaviors/actions of Car 
        public void Drive()=>Console.WriteLine("Car is driving with gear no" + gearNo);
        public void StartEngine()=>Console.WriteLine("Car engine starts");
        public void StopEngine()=>Console.WriteLine("Car engine stops");
        public void PlayHorn(int pressTime)
        {
            for (int i = 1; i <= pressTime; i++)
            {
                Console.WriteLine("T");
                Console.Beep();
            }
        }
        public static void GetModel()=>Console.WriteLine("Car Model is latest one");
    }
}