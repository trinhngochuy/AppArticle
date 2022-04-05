using ObserverPattern.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Web web = new Web();
            App app = new App();
            ModelFacade modelFacade = new ModelFacade();
            modelFacade.add(web);
            modelFacade.add(app);
            modelFacade.send();
            Console.ReadLine();
        }
    }
}
