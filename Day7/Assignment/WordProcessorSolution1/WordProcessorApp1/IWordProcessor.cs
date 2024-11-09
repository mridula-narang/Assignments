using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessorApp1
{
    internal interface IWordProcessor
    {
        string LongestWord(string[] words);
        string ShortestWord(string[] words);
        void ValidateInput(string[] words);
    }
}
