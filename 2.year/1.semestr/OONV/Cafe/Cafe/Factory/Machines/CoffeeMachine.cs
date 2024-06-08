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

        // Also note that, despite its name, the Creator's primary
        // responsibility is not creating products. Usually, it contains some
        // core business logic that relies on Product objects, returned by the
        // factory method. Subclasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of
        // product from it.
        public abstract ICoffee MakeCoffee();
    }
}
