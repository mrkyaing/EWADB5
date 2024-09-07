namespace OOP_Ensapsulation;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Student student=new Student();
            student.SetName("MG");
            student.SetEmail("mg@gmail.com");
            student.SetPhone("09256275319");
            student.SetAge(10);
            student.ShowInfo();

            Person person=new Person();
            person.Name="MG";
            person.Age=-30;
            person.ShowInfo();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occur because of "+e.Message);
        }
    }
}
