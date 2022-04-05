using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern.entity
{
    class ModelFacade
    {
        public ModelFacade()
        {

        }
        List<IObserve> observes;
        public void add(IObserve observe)
        {
            if (this.observes == null)
            {
                this.observes = new List<IObserve>();
            }
            observes.Add(observe);
          
        }
        public void remove(IObserve observe)
        {
            this.observes.Remove(observe);
           
        }

        public void send()
        {
            foreach (IObserve observe in this.observes)
            {
                observe.update();
            }
        }

    }
}
