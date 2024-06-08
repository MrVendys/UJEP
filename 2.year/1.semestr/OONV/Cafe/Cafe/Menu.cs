using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe
{
    internal class Menu
    {
       string path = @"C:\Users\vasik\OneDrive\Documents\Menu.txt";
       public Menu() {
            using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("1: Esspresso");
                    sw.WriteLine("2: Cafe Latte");
                    sw.WriteLine("3: Doppio");
                    sw.WriteLine("4: Cappucino");
                    sw.WriteLine("5: Flat White");
                }

        }
        public void ReadMenu()
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    
                }
            }
        }

    }
}
