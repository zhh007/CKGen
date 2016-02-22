using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public class ServiceLocator
    {
        private Dictionary<Type, object> dict = new Dictionary<Type, object>();

        private static ServiceLocator instance = new ServiceLocator();

        public static ServiceLocator Instance
        {
            get
            {
                return instance;
            }
        }

        public TService GetService<TService>()
        {
            return (TService)dict[typeof(TService)];
        }

        public void AddService<TService>(object instance)
        {
            dict[typeof(TService)] = instance;
        }
    }
}
