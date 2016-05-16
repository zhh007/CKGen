using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Events
{
    public class GetDbInstanceResponseEvent : CompositePresentationEvent<GetDbInstanceResponseEvent>
    {
        public object Token { get; set; }
        public IDatabaseInfo Database { get; set; }
    }
}
