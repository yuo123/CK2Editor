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
        public List<SectionEntry> Sections { get; private set; }

        public List<ValueEntry> Values { get; private set; }

        public SectionEntry()
        {
            Sections = new List<SectionEntry>();
            Values = new List<ValueEntry>();
        }

        public SectionEntry(string internalName, string friendlyName, string link)
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Link = link;
        }

        public void Save(StringBuilder sb, int indent = 0)
        {
            foreach (ValueEntry entry in Values)
            {
                FormatUtil.OutputValueFull(sb, entry, indent);
            }
            foreach (SectionEntry section in Sections)
            {
                FormatUtil.OutputSectionStart(sb, section.InternalName, indent);
                section.Save(sb, indent + 1);
                FormatUtil.OutputSectionEnd(sb, indent);
            }
        }

        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            Save(sb);
            return sb.ToString();
        }

        public List<Entry> Entries
        {
            get
            {
                var ret = new List<Entry>(Values.Count + Sections.Count);
                ret.AddRange(Values);
                ret.AddRange(Sections);
                return ret;
            }
        }
    }
}
