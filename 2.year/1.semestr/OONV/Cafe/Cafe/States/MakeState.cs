using Cafe.Factory.Machines;
namespace Cafe.States
{
    internal class MakeState : State
    {
        //Definice "MakeState"
        private CoffeeMachine machine;
        public override void onMaking(string order)
        {
            var coffee = "";
            if (order.Equals("1"))
            {
                machine = new EspressoMachine();
            }
            else if (order.Equals("2"))
            {
                machine = new LatteMachine();
            }
            else if (order.Equals("3"))
            {
                machine = new DoppioMachine();
            }
            else if (order.Equals("4"))
            {
                machine = new CappucinoMachine();
            }
            else if (order.Equals("5"))
            {
                machine = new FlatWhiteMachine();
            }
            else
            {
                worker.TransitionTo(new PayState());
            }
            coffee = machine.MakingCoffee(); ;

            Console.WriteLine($"Nápoj {coffee} je hotový.");

            worker.TransitionTo(new PayState());
        }

        public override void onPaying()
        {
            throw new NotImplementedException();
        }

        public override void onWaiting()
        {
            throw new NotImplementedException();
        }

    }
}
