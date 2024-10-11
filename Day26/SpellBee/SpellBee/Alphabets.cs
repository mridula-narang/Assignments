using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpellBee
{
    internal class Alphabets
    {
        public char CentreLetter { get; set; }
        public string SurroundingLetters { get; set; }
        public List<string> ValidWords { get; set; }
        public HashSet<string> UserWords { get; set; }
        public int TotalPoints { get; set; }

        public Alphabets()
        {
            CentreLetter = 'E';
            SurroundingLetters = "GHINWZ";
            ValidWords = new List<string>
            {
                //4 letter words
                "GEEZ","GENE","GHEE","HEWN","NENE","NINE","WHEE","WHEN","WHEW","WINE","ZINE",
                //5letter words,
                "GENIE","GENII","HENGE","HINGE","INNIE","NEIGH","WEIGH","WHINE","WIZEN",
                //6 letter words
                "EGGING","ENGINE","HEINIE","HEWING","WEENIE","WHEEZE","WHINGE","WIENIE",

                //7 letter words

                //8 letter words
                "NEIGHING","WEIGHING","WIZENING",
                //8 letter pangram
                "WHEEZING",
                //9 letter words
                "WHINGEING"
            };
            UserWords = new HashSet<string>();
            TotalPoints = 0;
        }
        public void Start()
        {
            Console.WriteLine("Welcome to the Spelling Bee!! Can you guess all the words? Guess all the words and become the queen bee");
            Console.WriteLine("The rules are simple: ");
            Console.WriteLine("1. You have to guess words that are at least 4 letters long");
            Console.WriteLine("2. The word must contain the centre letter");
            Console.WriteLine("3. The word must be made up of the surrounding letters");
            Console.WriteLine("4. The word must not be a duplicate");
            Console.WriteLine("5. The word must be a valid English word");
            Console.WriteLine("6. The word must be a pangram to get bonus points A word is a pangram if it contains all the letters.");
            Console.WriteLine("7. The game ends when you guess all the words or you enter 'exit'");
            Console.WriteLine("Lets begin!!");
            Console.WriteLine($"Your centre letter is {CentreLetter}");
            Console.WriteLine($"Your surrounding letters are {SurroundingLetters}");
            while (true)
            {
                string inputWord = GetUserInput();
                if (inputWord == "EXIT")
                {
                    break;
                }
                ProcessWord(inputWord);
            }
            EndGame();
        }

        public string GetUserInput()
        {
            Console.WriteLine("\n Enter a word or type exit to end");
            string word = Console.ReadLine().ToUpper();
            return word;
        }
        public void ProcessWord(string word)
        {
            if (UserWords.Contains(word))
            {
                Console.WriteLine("This word has already been entred");
                return;
            }
            if (WordValidator.IsValidWord(word, SurroundingLetters, CentreLetter,ValidWords,UserWords))
            {
                int points = PointCalculator.CalculatePoints(word,SurroundingLetters);
                TotalPoints += points;
                UserWords.Add(word);

                Console.WriteLine($"Valid word! You earned {points} points");
            }
            else
            {
                Console.WriteLine("Invalid word.");
            }
            Console.WriteLine($"Total points: {TotalPoints}");
        }
        public void EndGame()
        {
            Console.WriteLine("Game Over!!");
            Console.WriteLine($"You scored {TotalPoints} points");
            Console.WriteLine("Words you guessed: ");
            foreach (var word in UserWords)
            {
                Console.WriteLine(word);
            }
        }
    }
}
