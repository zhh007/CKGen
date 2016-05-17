using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Events
{
    public class AppEvent
    {
        private static IEventAggregator Events = new EventAggregator();

        public static void Subscribe<T>(Action<T> handler)
            where T : CompositePresentationEvent<T>, new()
        {
            Events.GetEvent<T>().Subscribe(handler);
        }

        public static void UnSubscribe<T>(Action<T> handler)
            where T : CompositePresentationEvent<T>, new()
        {
            Events.GetEvent<T>().UnSubscribe(handler);
        }

        public static void Publish<T>(T parameter)
            where T : CompositePresentationEvent<T>, new()
        {
            Events.GetEvent<T>().Publish(parameter);
        }
    }
}
