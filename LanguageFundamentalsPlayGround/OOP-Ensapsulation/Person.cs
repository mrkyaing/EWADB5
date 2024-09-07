using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Ensapsulation
{
    public class Person
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Invalid name");
                }
                name = value;
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Invalid Age");
                }
                age = value;
            }
        }
        public void ShowInfo()
        {
            Console.WriteLine("Person Information");
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Age:" + age);
        }
    }
}