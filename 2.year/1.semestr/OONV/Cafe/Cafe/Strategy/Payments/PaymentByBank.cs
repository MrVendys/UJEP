using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Strategy.Payments
{
    internal class PaymentByBank : IPaymentStrategy
    {
        public bool validatePaymentDetails()
        {
            Console.WriteLine("Posílání peněz");
            return true;
        }
        public void Pay()
        {
            Console.WriteLine("Zaplaceno převodem.");
        }

    }
}
