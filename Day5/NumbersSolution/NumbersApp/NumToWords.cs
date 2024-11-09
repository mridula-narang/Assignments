using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersApp
{
    internal class NumToWords
    {
        //Takes a number from user upto 9999 and prints the number in words
        public void NumberToWords()
        {
            string[] units = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "","","Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            Console.WriteLine("Please enter a number upto 9999");
            int num = Int32.Parse(Console.ReadLine());
            if (num > 9999)
            {
                Console.WriteLine("Number is too large. Please enter a number upto 9999 only");
                return;
            }
            if (num == 0)
            {
                Console.WriteLine("Zero");
            }
            string words = "";
            if (num<0)
            {
                words += "Negative ";
                num = Math.Abs(num);
            }
            if (num / 1000 > 0)
            {
                words += units[num / 1000] + " Thousand ";
                num %= 1000;
            }
            if (num / 100 > 0)
            {
                words += units[num / 100] + " Hundred ";
                num %= 100;
            }
            if (num >= 20)
            {
                words += tens[num / 10];
                num %= 10;
                if (num > 0)
                {
                    words += " " + units[num];
                }
            }
            else if (num >= 10)
            {
                words += teens[num - 10];
            }
            else if (num > 0)
            {
                words += units[num];
            }
            Console.WriteLine(words);
        }
    }
}
