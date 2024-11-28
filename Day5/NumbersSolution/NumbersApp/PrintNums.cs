using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersApp
{
    internal class PrintNums
    {
        //Prints numbers that end with 3 or divisible by 3
        public void NumbersOfThree() 
        {
            const int maxSize = 100;
            int[] arr = new int[maxSize];
            int count = 0;
            Console.WriteLine("Please enter numbers separated by spaces. Enter -1 to stop");
            string[] s = Console.ReadLine().Split(' ');
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == "-1")
                {
                    break;
                }
                arr[i] = Int32.Parse(s[i]);
                count++;
            }
            Console.WriteLine("Numbers that end with 3 or divisible by 3 are:");
            for (int i = 0; i < count; i++)
            {
                if (arr[i] % 3 == 0 || arr[i] % 10 == 3)
                {
                    Console.Write(arr[i] + " ");
                }
            }
            
        }
    }
}
