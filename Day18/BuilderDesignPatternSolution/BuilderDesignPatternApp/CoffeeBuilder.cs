using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesignPatternApp
{
    public class CoffeeBuilder : BeverageBuilder
    {
        public override void SetBeverageType()
        {
            Console.WriteLine("Coffee");
            GetBeverage().BeverageName = "Coffee";
        }

        public override void SetMilk()
        {
            Console.WriteLine("Step 2: Adding milk");
            GetBeverage().Milk = 50;
        }

        public override void SetPowderQuantity()
        {
            Console.WriteLine("Step 4: Adding 15gm coffee powder");
            GetBeverage().PowderQuantity = 15;
        }
        public override void SetSugar()
        {
            Console.WriteLine("Step 3: Adding sugar");
        }

        public override void SetWater()
        {
            Console.WriteLine("Step 1: Boiling water");
            GetBeverage().Water = 40;
        }
    }
}
