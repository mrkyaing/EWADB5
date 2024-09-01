namespace LanguageFundamentalsPlayGround;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        int i = 10;
        float f = 200.3f;
        Console.WriteLine("i value is " + i);//i value is 10
        Console.WriteLine("f:{0},i:{1}", f, i);//f:200.3,i:10
        string name = "MG";
        Console.WriteLine($"Hello,{name}.");//Hello,MG
        if (i > 20)
        {
            Console.WriteLine("I come from if statement.");
            Console.WriteLine("i is greater than 20.");
        }
        else
        {
            Console.WriteLine("I come from else statement.");
            Console.WriteLine("i is less than 20.");
        }
        char aChar = 'z';
        switch (aChar)
        {
            case 'A': Console.WriteLine($"{aChar} is vowel."); break;
            case 'a': Console.WriteLine($"{aChar}  is vowel."); break;
            case 'E': Console.WriteLine($"{aChar}  is vowel."); break;
            case 'e': Console.WriteLine($"{aChar}  is vowel."); break;
            default: Console.WriteLine($"{aChar}  is not voewl."); break;
        }
        string Messsages = "Hello,How are you? nice to see you.I am from YGN.I am attending in EWAD course.";
        int totalVowelCount = 0;
        Messsages = Messsages.ToLower();
        for (int j = 0; j < Messsages.Length; j++)
        {
            switch (Messsages[j])
            {//H l l o , H i
                case 'a': totalVowelCount++; break;
                case 'e': totalVowelCount++; break;
                case 'i': totalVowelCount++; break;
                case 'o': totalVowelCount++; break;
                case 'u': totalVowelCount++; break;
            }//end of switch 
        }//end of for
        DateTime dob = new DateTime(1997, 8, 8);
        Console.WriteLine("my DOB:", dob.Date);
        Console.WriteLine("Year :" + dob.Year);//1997
        Console.WriteLine($"Total vowel Count in Message:{totalVowelCount}");
        int count = 1;
        while (count <= 10)
        {
            Console.WriteLine($"#:{count}");
            count++;
        }//#:1
        //#:2
        Console.WriteLine("=================");
        int c = 1;
        do
        {
            Console.WriteLine($"Do-While Loop#:{c}");
            c++;
        } while (c <= 10);
        string Friends = "MG MG,SU SU";
        Console.WriteLine("Demo for loop");
        for (int k = 0; k < Friends.Length; k++)
        {
            Console.WriteLine(Friends[k]);
        }
        Console.WriteLine("Demo foreach loop");
        foreach (char friend in Friends)
        {
            Console.WriteLine(friend);
        }
        Console.WriteLine("Demo foreach loop");
        int[] Marks = { 1, 2, 3 };
        foreach (int m in Marks)
        {
            Console.Write(m + " ");//1 2 3 
        }
        decimal taxResult = TotalTax(30000.36m, 3);
        Console.WriteLine("Total Tax Amount:" + taxResult);//1.0  
        string[] WorkingDays = { "Monday", "Tuesday", "WednessDay", "Thursday", "Friday" };
        foreach (string w in WorkingDays)
        {
            if (w.Equals("Monday"))
            {
                goto BusyDay;
            }
            else if (w.Equals("Tuesday"))
            {
                Console.WriteLine("Working as a little busy");
            }
            else
            {
                Console.WriteLine("Doing the work as normal");
            }
        }
    BusyDay:
        {//Label Statement in C#
            Console.WriteLine("Meeting at 09:00am");
        }
        int[] Numbers = { 1, 2, 3, 5, 7, 8, 10, 11, 12, 13, 14, 15 };
        foreach (var n in Numbers)
        {
            if (n % 2 == 0)
            {
                if (n == 10)
                {
                    //continue;//Skip
                    break;
                }
                Console.WriteLine("Even:" + n);
            }
        }
    }//end of Main
    
    // static keyword can be used infront of classes,variables,methods.
    public static decimal TotalTax(decimal cost, int Perncent)
    {
        //return 1.0m;
        return cost / 100 * Perncent;
    }//end of TotalTax
}
