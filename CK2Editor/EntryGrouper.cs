using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor
{
    /// <summary>
    /// Represents a section that is only an abstract part of the tree and does not have a physical presence in the save file
    /// </summary>
    public class EntryGrouper : SectionEntry
    {
        public override void Save(StringBuilder sb, int indent = 0)
        {
            SaveContent(sb, indent);
            sb.Length--;//get rid of the trailing line break
        }

        protected override void SaveHeader(StringBuilder sb, int indent)
        {
            return; //A grouper has no header
        }

        protected override void SaveFooter(StringBuilder sb, int indent)
        {
            return; //A grouper has no footer
        }
    }
}
