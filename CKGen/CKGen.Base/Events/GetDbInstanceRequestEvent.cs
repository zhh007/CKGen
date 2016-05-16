using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Events
{
    public class GetDbInstanceRequestEvent : CompositePresentationEvent<GetDbInstanceRequestEvent>
    {
        public object Token { get; set; }
    }
}
