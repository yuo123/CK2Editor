using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor.Utility;

namespace CK2Editor
{
    public class SectionEntry : Entry
    {
        /// <summary>
        /// Specifies whether this section is only an abstract part of the tree and does not have a physical presence in the save file
        /// </summary>
        public virtual bool IsGrouper { get { return false; } }

        public SectionEntry()
        {
            Entries = new List<Entry>();
        }

        public SectionEntry(string internalName, string friendlyName, string link)
            : this()
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Link = link;
        }

        /// <summary>
        /// Outputs the text representation of this section, as it would appear in a save file
        /// </summary>
        /// <param name="sb">The string builder to append to</param>
        /// <param name="indent">the base indent of this section inside the parent section</param>
        public void Save(StringBuilder sb, int indent = 0)
        {
            foreach (Entry entry in Entries)
            {
                if (entry is ValueEntry)
                {
                    FormatUtil.OutputValueFull(sb, (ValueEntry)entry, indent);
                }
                else if (entry is SectionEntry)
                {
                    SectionEntry sEntry = ((SectionEntry)entry);
                    if (sEntry.IsGrouper)
                        sEntry.Save(sb, indent);
                    else
                    {
                        FormatUtil.OutputSectionStart(sb, entry.InternalName, indent);
                        sEntry.Save(sb, indent + 1);
                        FormatUtil.OutputSectionEnd(sb, indent);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the text representation of this section, as it would appear in a save file
        /// </summary>
        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            Save(sb);
            return sb.ToString();
        }

        /// <summary>
        /// The collection of all the entries which are contained within this section
        /// </summary>
        public List<Entry> Entries { get; private set; }

        public override bool Equals(Entry other)
        {
            SectionEntry others = other as SectionEntry;
            if (others == null || !base.Equals(other))
                return false;
            var pairs = this.Entries.Zip(others.Entries, (a, b) => new { A = a, B = b });
            foreach (var pair in pairs)
            {
                if (!pair.A.Equals(pair.B))
                    return false;
            }
            return true;
        }
    }
}
