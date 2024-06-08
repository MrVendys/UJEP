﻿using Cafe.States;
using DotLiquid.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe
{
    internal class CafeWorker
    {
        private State state;
        public CafeWorker(State state)
        {
            this.TransitionTo(state);
        }
        public void TransitionTo(State state)
        {
            //Console.WriteLine($"Context: Transition to {state.GetType().Name}.");
            this.state = state;
            this.state.SetState(this);
        }

        // The Context delegates part of its behavior to the current State
        // object.
        public void WaitingForCustomer()
        {
            this.state.onWaiting();
        }

        public void MakeCoffee(String s)
        {
            this.state.onMaking(s);
        }
        public void CustomerPaying()
        {
            this.state.onPaying();
        }
    }
    
}
