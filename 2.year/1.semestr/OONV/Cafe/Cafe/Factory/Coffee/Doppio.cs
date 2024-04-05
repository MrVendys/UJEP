using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Factory.Coffee
{
    internal class Doppio : ICoffee
    {
        public string Prepare()
        {
            return "Doppio";
        }
    }
}
