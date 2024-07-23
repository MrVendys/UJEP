using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafe.States;

namespace Cafe
{
    public sealed class CafeShop
    {
        private CafeWorker worker;
        private Menu menu;
        private CafeShop()
        {

        }
        //Implementace vzoru "Singleton"
        private static CafeShop instance;
        public static CafeShop GetInstance()
        {
            if (instance == null)
            {
                instance = new CafeShop();
            }
            return instance;
        }
        public void Open()
        {
            //Vytvoření třídy worker a menu
            //Workerovi rovnou přiřadit stav "Čekat na zákazníka"
            worker = new CafeWorker(new WaitState());
            menu = new Menu();
            worker.WaitingForCustomer();
        }
        //Funkce spracovávající vstup uživatele a měnění stavu Workera
        public void CustomerComes()
        {
            Console.WriteLine("Dobrý den. Vítejte v kávovém stánku ForRest Cafe, copak si dáte? (menu)");
            while (true)
            {
                string answer = Console.ReadLine().ToLower();
                if (answer.Equals("menu"))
                {
                    menu.ReadMenu();
                    
                    answer = Console.ReadLine().ToLower();
                    if (!answer.Equals(""))
                        worker.MakeCoffee(answer);
                    else
                        continue;
                }
                else if (!answer.Equals(""))
                    worker.MakeCoffee(answer);
                else
                    continue;
                worker.CustomerPaying();
            }
        }
        

    }
    
}
