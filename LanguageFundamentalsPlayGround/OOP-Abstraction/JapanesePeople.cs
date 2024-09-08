namespace OOP_Abstraction
{
    public class JapanesePeople : SayHello
    {
        public override void AboutMe()
        {
            Console.WriteLine($"Kon'nichiwa, watashi wa {base.Name}desu. Genzai, {base.Address} ni sunde imasu");
        }

        public override void NotifyMeWhenYouMeetAnyWhere()
        {
            Console.WriteLine("元気ですか");
        }

        public override void SayGreetingMessage()
        {
            Console.WriteLine("Kon'nichiwa, o ai dekite ureshīdesu");
        }
    }
}