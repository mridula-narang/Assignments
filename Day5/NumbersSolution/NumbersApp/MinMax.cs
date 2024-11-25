using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersApp
{
    internal class MinMax
    {
        //Finds prime numbers between a given range
        public void FindPrimeNumbers()
        {
            Console.WriteLine("Please enter the minimum value");
            int min = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the maximum value");
            int max = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Prime numbers between " + min + " and " + max + " are:");
            for (int i = min; i <= max; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write(i + " ");
                }
            }
        }
        public bool IsPrime(int i) 
        {
            if (i <= 1)
            {
                return false;
            }
            for (int j = 2; j <= Math.Sqrt(i); j++)
            {
                if (i % j == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
