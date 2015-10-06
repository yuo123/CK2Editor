using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CK2Editor.Utility;

namespace CK2Editor.Editors
{
    public class Editor : IEditor
    {
        public IList<SectionEntry> Sections { get; private set; }

        public IList<ValueEntry> Values { get; private set; }

        public Editor()
        {
            Sections = new List<SectionEntry>();
            Values = new List<ValueEntry>();
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
                section.Section.Save(sb, indent + 1);
                FormatUtil.OutputSectionEnd(sb, indent);
            }
        }

        public string Save()
        {
            StringBuilder sb = new StringBuilder();
            Save(sb);
            return sb.ToString();
        }


        public IEditor Root { get; set; }


        public IList<Entry> Entries
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
