using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Factory.Coffee;

namespace Cafe.Factory.Machines
{
    abstract class CoffeeMachine
    {
        public string MakingCoffee()
        {
            var product = MakeCoffee().Prepare();
            var result = product;
            return result;
        }

        public abstract ICoffee MakeCoffee();
    }
}
