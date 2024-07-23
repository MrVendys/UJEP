using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.States
{
    //Definice WaitState
    internal class WaitState : State
    {
        CafeShop CafeShop = CafeShop.GetInstance();
        public override void onMaking(string s)
        {
            throw new NotImplementedException();
        }

        public override void onPaying()
        {
            throw new NotImplementedException();
        }

        public override void onWaiting()
        {
            Console.WriteLine("Čekám na zákazníka.");
            while (true)
            {
                if (Console.ReadLine() == "")
                {
                    worker.TransitionTo(new MakeState());
                    CafeShop.CustomerComes();
                    break;
                }
            }
        }
    }
}
