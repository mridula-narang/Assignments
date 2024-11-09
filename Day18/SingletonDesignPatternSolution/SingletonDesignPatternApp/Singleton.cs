using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonDesignPatternApp
{
    public sealed class Singleton
    {
        private static int Counter = 0;
        private static Singleton Instance = null;
        public static Singleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Singleton();
            }
            return Instance;
        }
        private Singleton()
        {
            Counter++;
            Console.WriteLine("Counter Value " + Counter.ToString());
        }
        public void PrintDetails(string message)
        {
            Console.WriteLine(message);
        }
    }
}
