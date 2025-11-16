using System;

namespace Fractions
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction f1 = new Fraction();
            Fraction f2 = new Fraction(5);
            Fraction f3 = new Fraction(6, 7);

            Console.WriteLine(f1.GetFractionString()); 
            Console.WriteLine(f1.GetDecimalValue());   

            Console.WriteLine(f2.GetFractionString()); 
            Console.WriteLine(f2.GetDecimalValue());  

            Console.WriteLine(f3.GetFractionString()); 
            Console.WriteLine(f3.GetDecimalValue());  

            
            f3.SetTop(3);
            f3.SetBottom(4);
            Console.WriteLine(f3.GetFractionString()); 
            Console.WriteLine(f3.GetDecimalValue());   
        }
    }
}
