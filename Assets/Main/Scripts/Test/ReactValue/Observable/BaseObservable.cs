using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Main.Scripts.Test.ReactValue
{
    public abstract class BaseObservable : IObservable
    {
        protected List<IObserver> Observers;

        public BaseObservable()
        {
            Observers = new List<IObserver>();
        }

        public virtual void Add(IObserver observer)
        {
            Observers.Add(observer);
        }

        public void Notify()
        {
            Observers.ForEach(observer => observer.React());
        }

        public void Remove(IObserver observer)
        {
            Observers.Remove(observer);
        }
    }
}
