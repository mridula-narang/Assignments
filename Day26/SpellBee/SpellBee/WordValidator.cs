using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellBee
{
    internal class WordValidator
    {
        public static bool IsValidWord(string word, string surroundingLetters, char centreLetter, List<string> validWords, HashSet<string> userWords)
        {
            // Convert the word to uppercase for validation
            string upperWord = word.ToUpper();

            if (upperWord.Length < 4 || !upperWord.Contains(char.ToUpper(centreLetter)))
            {
                return false;
            }

            if (validWords.Contains(upperWord))
            {
                return true;
            }
            return false;
        }
    }
}
