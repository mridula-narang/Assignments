using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderDesignPatternApp
{
    public class TeaBuilder : BeverageBuilder
    {
        public override void SetBeverageType()
        {
            Console.WriteLine("Tea");
            GetBeverage().BeverageName = "Tea";
        }

        public override void SetMilk()
        {
            Console.WriteLine("Step 2: Adding milk");
            GetBeverage().Milk = 80;
        }

        public override void SetPowderQuantity()
        {
            Console.WriteLine("Step 4: Adding 20g of tea leaves");
            GetBeverage().PowderQuantity = 20;
        }

        public override void SetSugar()
        {
            Console.WriteLine("Step 3: Adding sugar");
            GetBeverage().Sugar = 15;
        }

        public override void SetWater()
        {
            Console.WriteLine("Step 1: Boiling water");
            GetBeverage().Water = 60;
        }
    }
}
