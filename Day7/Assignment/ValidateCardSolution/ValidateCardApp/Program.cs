namespace ValidateCardApp
{
    internal class Program
    {
        //implement the validation of the card number by taking user input
        static bool ValidateCard(string cardNumber)
        {
            // Step 1: Reverse the card number
            string reversedCardNumber = ReverseString(cardNumber);

            int sum = 0;

            // Step 2: Loop through the reversed card number
            for (int i = 0; i < reversedCardNumber.Length; i++)
            {
                int currentDigit = int.Parse(reversedCardNumber[i].ToString());

                // Step 3: Identify even positions (1-based index, but 0-based loop)
                if (i % 2 == 1)
                {
                    currentDigit *= 2;

                    // Step 4: If the result is a two-digit number, sum the digits
                    if (currentDigit > 9)
                    {
                        currentDigit = currentDigit - 9; // Same as summing digits of the number
                    }
                }

                // Add the current digit to the sum
                sum += currentDigit;
            }

            // Step 5: Check if sum is divisible by 10
            return (sum % 10 == 0);
        }

        static string ReverseString(string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter card number without spaces");
            string cardNumber = Console.ReadLine();
            bool isValid = ValidateCard(cardNumber);

            if (isValid)
                Console.WriteLine("The card number is valid.");
            else
                Console.WriteLine("The card number is invalid.");
        }
    }
}
