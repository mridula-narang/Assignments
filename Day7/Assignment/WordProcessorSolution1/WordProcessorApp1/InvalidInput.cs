using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordProcessorApp1
{
    public class InvalidInput : Exception
    {
        string message;
        public InvalidInput() 
        {
            message = "Input provided is numbers which is invalid. Please enter words.";
        }
    }
}
