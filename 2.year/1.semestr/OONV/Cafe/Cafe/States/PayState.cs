using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.Strategy.Payments;

namespace Cafe.States
{
    internal class PayState : State
    {
        private IPaymentStrategy _strategy;
        public override void onMaking(string s)
        {
            throw new NotImplementedException();
        }

        public override void onPaying()
        {
            bool pay = false;
            Console.WriteLine("Budete platit kartou, hotově nebo převodem na účet?");
            while (!pay)
            {
                Console.WriteLine("1: Kreditní kartou\n2: Hotově\n3: Bankovním převodem");
                string answer = Console.ReadLine();
                if (answer.Equals("1"))
                {
                    _strategy = new PaymentByCreditCard();
                }

                else if (answer.Equals("2"))
                {
                    _strategy = new PaymentByCash();
                }
                else
                {
                    _strategy = new PaymentByBank();
                }
                if (_strategy.validatePaymentDetails())
                {
                    _strategy.Pay();
                    pay = true;
                    Console.WriteLine("Děkujeme a nashledanou!");
                    worker.TransitionTo(new WaitState());
                    worker.WaitingForCustomer();
                }
                else
                    continue;
            }

        }

        public override void onWaiting()
        {
            throw new NotImplementedException();
        }
    }
}
