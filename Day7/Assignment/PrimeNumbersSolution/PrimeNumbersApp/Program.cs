namespace PrimeNumbersApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrimeNumbers primeNumbers = new PrimeNumbers();
            StorePrimes storePrimes = new StorePrimes();
            while (true)
            {
                Console.Write("Enter a number (0 to stop): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number))
                {
                    if (number == 0)
                        break;

                    if (primeNumbers.IsPrime(number))
                    {
                        storePrimes.AddPrimeNumber(number);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }

            storePrimes.PrintPrimes();
        }
    }
}
