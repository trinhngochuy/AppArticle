using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.entity
{
    class App : IObserve
    {
        public void update()
        {
            Console.WriteLine("app da thay doi");
        }
    }
}
