using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    [Export(typeof(IEventAggregator))]
    public class EventAggregator : IEventAggregator
    {
        private List<EventBase> events = new List<EventBase>();

        public T GetEvent<T>()
        {
            if(events.OfType<T>().FirstOrDefault() == null)
            {
                var evt = Activator.CreateInstance<T>();
                events.Add(evt as EventBase);
            }

            var result = events.OfType<T>().FirstOrDefault();
            return result;
        }
    }
}
