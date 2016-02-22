using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base
{
    public interface IEventAggregator
    {
        T GetEvent<T>();
    }
}
