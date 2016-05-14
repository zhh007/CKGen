using CKGen.Base;
using CKGen.DBSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Base.Events
{
    public class DatabaseRefreshEvent : CompositePresentationEvent<DatabaseRefreshEvent>
    {
        public IDatabaseInfo Database { get; set; }
    }
}
