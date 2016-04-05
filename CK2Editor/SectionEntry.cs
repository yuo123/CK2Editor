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
                    FormatUtil.OutputSectionStart(sb, entry.InternalName, indent);
                    ((SectionEntry)entry).Save(sb, indent + 1);
                    FormatUtil.OutputSectionEnd(sb, indent);
                }
            }
        }

        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            Save(sb);
            return sb.ToString();
        }

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
