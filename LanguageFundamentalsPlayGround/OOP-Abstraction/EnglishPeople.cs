namespace OOP_Abstraction
{
    public class EnglishPeople : SayHello
    {
        public override void AboutMe()
        {
            Console.WriteLine("Hi,I am "+base.Name+".Curreltly,I live in "+base.Address);
        }

        public override void NotifyMeWhenYouMeetAnyWhere()
        {
           Console.WriteLine("Hay,"+base.Name+"How is going and please to meet you.");
        }

        public override void SayGreetingMessage()
        {
          Console.WriteLine("Hello,Nice to see you");
        }
    }
}