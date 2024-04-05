using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Factory.Coffee;

namespace Cafe.Factory.Machines
{
    internal class CappucinoMachine : CoffeeMachine
    {
        public override ICoffee MakeCoffee()
        {
            Console.WriteLine("Making Cappucino..... vrrrrrrrr....pššchchchch...");
            return new Cappucino();
        }
    }
}
