using System.Text.Json.Serialization;

namespace OOP_Abstraction;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Abstraction Demo");
        EnglishPeople english = new EnglishPeople()
        {
            Name = "Jame",
            Address = "USA"
        };
        english.SayGreetingMessage();
        english.NotifyMeWhenYouMeetAnyWhere();
        english.AboutMe();
        english.ShowInfo();

        MyanmarPeople myanmar = new MyanmarPeople()
        {
            Name = "MG",
            Address = "YGN"
        };
        myanmar.SayGreetingMessage();
        myanmar.NotifyMeWhenYouMeetAnyWhere();
        myanmar.AboutMe();
        myanmar.ShowInfo();

        JapanesePeople japanese = new JapanesePeople()
        {
            Name = "Khonoshi",
            Address = "TOKAYO"
        };
        japanese.SayGreetingMessage();
        japanese.NotifyMeWhenYouMeetAnyWhere();
        japanese.AboutMe();
        japanese.ShowInfo();
    }
}
