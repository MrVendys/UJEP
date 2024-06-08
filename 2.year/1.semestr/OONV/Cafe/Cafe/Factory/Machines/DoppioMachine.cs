using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Factory.Coffee;

namespace Cafe.Factory.Machines
{
    internal class DoppioMachine : CoffeeMachine
    {
        public override ICoffee MakeCoffee()
        {
            Console.WriteLine("Making Doppio.... vrrrrrrvrrrrrrrrrrrrrrrrrrrrr");
            return new CaffeLatte();
        }
    }
}
