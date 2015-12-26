using CKGen.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKGen.Events
{
    public class ShowCodeEvent : CompositePresentationEvent<ShowCodeEvent>
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
