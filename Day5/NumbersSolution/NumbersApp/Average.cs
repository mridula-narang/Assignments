using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersApp
{
    internal class Average
    {
        public void CalculateAverage()
        {
            //Take 10 numbers from user and calculates the average of numbers divisible by 7
            int sum = 0;
            int count = 0;
            int[] arr = new int[10];
            Console.WriteLine("Please enter 10 numbers separated by spaces");
            string[] s = Console.ReadLine().Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.Parse(s[i]);
            }
            foreach (var item in arr)
            {
                if (item % 7 == 0)
                {
                    sum += item;
                    count++;
                }
            }
            if (count > 0)
            {
                Console.WriteLine("Average of numbers divisible by 7 is: " + (sum / count));
            }
            else
            {
                Console.WriteLine("No numbers are divisible by 7");
            }
        }
    }
}
