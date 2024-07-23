using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.States
{
    //Děděná třída State
    abstract class State
    {
        protected CafeWorker worker;

        public void SetState(CafeWorker worker)
        {
            this.worker = worker;
        }
        public abstract void onWaiting();
        public abstract void onMaking(string s);
        public abstract void onPaying();

    }
}
