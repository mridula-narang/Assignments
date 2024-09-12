using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessorApp1
{
    internal class WordProcessor : IWordProcessor
    {
        public string LongestWord(string[] words)
        {
            if (words.Length == 0)
            {
                throw new Exception("No words to process");
            }
            string longestWord = words[0];
            foreach (string word in words)
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
            return longestWord;
        }
        public string ShortestWord(string[] words)
        {
            if (words.Length == 0)
            {
                throw new Exception("No words to process");
            }
            string shortestWord = words[0];
            foreach (string word in words)
            {
                if (word.Length < shortestWord.Length)
                {
                    shortestWord = word;
                }
            }
            return shortestWord;
        }
        public void ValidateInput(string[] words)
        {
            foreach (var word in words)
            {
                if (int.TryParse(word, out _))
                {
                    throw new InvalidInput();
                }
            }
        }
    }
}
