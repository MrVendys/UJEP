using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Factory.Coffee;

namespace Cafe.Factory.Machines
{
    internal class LatteMachine : CoffeeMachine
    {
        public override ICoffee MakeCoffee()
        {
            Console.WriteLine("Making Caffe Latte.....vrrrrrrrrrrr.... pššchchchchchchchchch...");
            return new CaffeLatte();
        }
    }
}
