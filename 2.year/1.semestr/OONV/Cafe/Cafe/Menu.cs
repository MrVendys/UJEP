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
        //Definice cesty pro uložení txt souboru
        string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string fileName = "Menu.txt";
        string path;
       public Menu() {
            //Vytváření souboru "Menu" a zapisování
            path = Path.Combine(documentPath, fileName);
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
            //Otevření souboru pro čtení
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
