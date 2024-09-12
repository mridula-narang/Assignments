namespace WordProcessorApp2
{
    internal class Program
    {
        // Function to count repeating vowels in a word
        static int CountRepeatingVowels(string word)
        {
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            HashSet<char> foundVowels = new HashSet<char>();
            int count = 0;

            foreach (char ch in word.ToLower())
            {
                if (vowels.Contains(ch))
                {
                    if (foundVowels.Contains(ch))
                    {
                        count++; // It's a repeating vowel
                    }
                    else
                    {
                        foundVowels.Add(ch); // Mark the vowel as found
                    }
                }
            }

            return count;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter words separated by commas:");
            string input = Console.ReadLine();

            // Split the input string by commas
            string[] words = input.Split(',');

            // Find the word(s) with the least number of repeating vowels
            List<string> result = new List<string>();
            int minRepeatingVowels = int.MaxValue;

            foreach (string word in words)
            {
                int repeatCount = CountRepeatingVowels(word.Trim());
                if (repeatCount < minRepeatingVowels)
                {
                    minRepeatingVowels = repeatCount;
                    result.Clear();
                    result.Add(word.Trim());
                }
                else if (repeatCount == minRepeatingVowels)
                {
                    result.Add(word.Trim());
                }
            }

            // Print the result
            Console.WriteLine(string.Join(" ", result));
        }

    }
}