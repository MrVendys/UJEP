﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Strategy.Payments
{
    public interface IPaymentStrategy
    {
        void Pay();
        public bool validatePaymentDetails();
    }
}
