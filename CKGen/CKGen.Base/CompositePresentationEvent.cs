using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public class CompositePresentationEvent<T> : EventBase
        where T : new()
    {
        private List<Action<T>> handlers = new List<Action<T>>();

        public void Subscribe(Action<T> handler)
        {
            handlers.Add(handler);
        }

        public void Publish(T parameter)
        {
            handlers.ForEach(a => a(parameter));
        }
    }
}
