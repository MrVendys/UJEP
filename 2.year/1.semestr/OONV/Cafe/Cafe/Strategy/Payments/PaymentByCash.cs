using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Strategy.Payments
{
    internal class PaymentByCash : IPaymentStrategy
    {
        public bool validatePaymentDetails()
        {
            Console.WriteLine("Počítání peněz");
            Random r = new Random();
            if (r.NextDouble() >= 0.2)
            {
                Console.WriteLine("Peníze v pořádku.");
                return true;
            }
            else
            {
                Console.WriteLine("Nedal jste mi dostatek.");
                return false;
            }
        }

        public void Pay()
        {
            Console.WriteLine("Zaplaceno v hotovosti.");
        }

    }
}
