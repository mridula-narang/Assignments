using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeNumbersApp
{
    public class PrimeNumbers
    {
        public bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            if (number == 2)
                return true;
            if (number % 2 == 0)
                return false;

            for (int i = 3; i * i <= number; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
    public class StorePrimes
    {
        private List<int> primeNumbers = new List<int>();

        public void AddPrimeNumber(int number)
        {
            primeNumbers.Add(number);
        }

        public void PrintPrimes()
        {
            Console.WriteLine("Prime numbers entered:");
            if (primeNumbers.Count > 0)
            {
                foreach (var prime in primeNumbers)
                {
                    Console.WriteLine(prime);
                }
            }
            else
            {
                Console.WriteLine("No prime numbers were entered.");
            }
        }

    }


