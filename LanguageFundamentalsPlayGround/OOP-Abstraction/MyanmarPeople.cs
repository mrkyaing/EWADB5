namespace OOP_Abstraction
{
    public class MyanmarPeople : SayHello
    {
        public override void AboutMe()
        {
            Console.WriteLine("ဟုတ်ကဲ့.ကျနော်နာမည်" + base.Name + " ဖြစ်ပါတယ်။ အခု လက်ရှိ" + base.Address + "မှာ နေပါတယ်ခင်ဗျ.");
        }

        public override void NotifyMeWhenYouMeetAnyWhere()
        {
            Console.WriteLine("ဟေး မင်းနေကောင်းလား ဘာတွေလုပ်နေလဲ.");
        }

        public override void SayGreetingMessage()
        {
            Console.WriteLine("မင်္ဂလာပါခင်ဗျ.");
        }
    }
}