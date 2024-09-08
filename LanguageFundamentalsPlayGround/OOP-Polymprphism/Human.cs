using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Polymprphism
{
    public class Human:Mammal
    {
        public string BloodType { get; set; }
        public string NRC { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string WorkType { get; set; }
        public override void Sound()
        {
            Console.WriteLine($"Hello,I am {base.Name}");
        }
        public override string ToString()
        {
            base.ToString();
            return $"BloodType:{BloodType}\nNRC:{NRC}\nPhone:{Phone}\nWorkType{WorkType}\nAddress:{Address}";
        }
    }
}