using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Inheritance
{
    public class Cat:Animal
    {       
        public override void MakeSound()=>Console.WriteLine(base.Name+"is making soung as Meow..");
        public void SayHello(){

        }
         public void SayHello(int i){
            
        }
    }
}