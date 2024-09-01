Console.WriteLine("Hello, World!");
Person p = new Person();
try
{
    p.SayHello("MG");
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e.Message);
}
