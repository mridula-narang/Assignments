namespace WordProcessorApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
			try
			{
                Console.WriteLine("Enter words separated by commas:");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException("Input cannot be empty or null.");
                }

                string[] words = input.Split(',');
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }
                IWordProcessor processor = new WordProcessor();
                processor.ValidateInput(words);
                string longestWord = processor.LongestWord(words);
                string shortestWord = processor.ShortestWord(words);

                Console.WriteLine($"Longest word: {longestWord}");
                Console.WriteLine($"Shortest word: {shortestWord}");
            }
			catch (ArgumentException ex)
			{
                Console.WriteLine("Input cannot be empty or null. Please provide an input");
			}
            catch (InvalidInput ex)
            {
                Console.WriteLine("Please enter words only. Numbers are not accepted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
