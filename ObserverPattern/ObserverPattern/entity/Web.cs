using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.entity
{
    class Web : IObserve
    {
        public void update()
        {
            Console.WriteLine("web thay doi ");
        }
    }
}
