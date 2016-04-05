using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    public class EntryEventArgs : EventArgs
    {
        public Entry Entry { get; set; }
        public EntryEventArgs() { }
        public EntryEventArgs(Entry entry)
        {
            this.Entry = entry;
        }
    }
}
