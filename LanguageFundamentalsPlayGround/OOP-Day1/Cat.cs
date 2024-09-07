namespace OOP_Day1
{
    class Cat
    {
        public void Sound()
        {
            Console.WriteLine("Weow");
        }
        public void Sound(int time)
        {
            for (int i = 1; i <= time; i++)
            {
                Console.WriteLine("Weow");
            }
        }
    }
}