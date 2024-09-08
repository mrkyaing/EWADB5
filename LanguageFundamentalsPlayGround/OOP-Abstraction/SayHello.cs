namespace OOP_Abstraction
{
    public abstract class SayHello
    {
        public string Name { get; set; }
        public string Address { get; set; }

        //Non-abstract Method
        public void ShowInfo()
        {
            Console.WriteLine("Name:" + Name);
            Console.WriteLine("Address:" + Address);
        }
        //abstract Methods
        public abstract void SayGreetingMessage();
        public abstract void AboutMe();
        public abstract void NotifyMeWhenYouMeetAnyWhere();
    }
}