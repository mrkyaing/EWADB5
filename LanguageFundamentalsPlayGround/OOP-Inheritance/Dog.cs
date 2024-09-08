using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Inheritance
{
    public class Dog : Animal
    {  
        //method override in here 
        //one of polymorphism in here 
        public override void MakeSound(){
            base.MakeSound();
            Console.WriteLine("Woak");
        } 
    }
}