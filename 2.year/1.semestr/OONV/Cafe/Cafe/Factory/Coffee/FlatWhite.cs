using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Factory.Coffee
{
    internal class FlatWhite : ICoffee
    {
        public string Prepare()
        {
            return "FlatWhite";
        }
    }
}
