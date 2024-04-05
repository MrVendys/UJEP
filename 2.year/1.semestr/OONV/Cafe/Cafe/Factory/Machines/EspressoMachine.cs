using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Factory.Coffee;

namespace Cafe.Factory.Machines
{
    internal class EspressoMachine : CoffeeMachine
    {
        public override ICoffee MakeCoffee()
        {
            Console.WriteLine("Making Espresso.... vrrrrrrvrrrrrr");
            return new Espresso();
        }
    }
}
