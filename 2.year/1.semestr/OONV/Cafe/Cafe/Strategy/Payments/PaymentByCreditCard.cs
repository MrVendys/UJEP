using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Strategy.Payments
{
    internal class PaymentByCreditCard : IPaymentStrategy
    {
        private string creditCardNumber;
        private string creditCardExp;
        private string creditCardCVV;

        private Random r = new Random();
        public bool validatePaymentDetails()
        {
            Console.WriteLine("Čtení karty");
            if (r.NextDouble() >= 0.2)
            {

                Console.WriteLine("Karta přijata");
                return true;
            }
            else
            {
                Console.WriteLine("Karta zamítnuta, špatná karta");
                Console.WriteLine("Prosím, zkuste přiložit znovu. Když tak můžete zaplatit hotově nebo převodem na účet.");
                return false;
            }
        }
        public void Pay()
        {
            Console.WriteLine("Zaplaceno kreditní kartou.");
        }
    }
}
