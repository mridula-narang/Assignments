using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellBee
{
    internal class PointCalculator
    {
        public static int CalculatePoints(string word, string surroundingLetters)
        {
            int length = word.Length;
            switch (length)
            {
                case 4:
                    return 1;
                case 5:
                    return 5;
                case 6:
                    return 6;
                case 8:
                    if (IsPangram(word))
                    {
                        return 16;
                    }
                    return 8;
                case 9:
                    return 9;
                default:
                    return 0;
            }
        }

        private static bool IsPangram(string word)
        {
            string letters = "EGHINWZ";
            foreach (char c in letters)
            {
                if (!word.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
