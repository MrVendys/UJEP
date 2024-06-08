﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Factory.Coffee
{
    public class Espresso : ICoffee
    {
        public string Prepare()
        {
            return "Espresso";
        }
    }
}
