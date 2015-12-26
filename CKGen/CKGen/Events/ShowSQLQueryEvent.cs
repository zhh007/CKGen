using CKGen.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Events
{
    public class ShowSQLQueryEvent : CompositePresentationEvent<ShowSQLQueryEvent>
    {
        public string SQL { get; set; }
    }
}
