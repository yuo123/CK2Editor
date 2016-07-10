using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    public class EntryGrouper : SectionEntry
    {
        public override bool IsGrouper { get { return true; } }
    }
}
