using System;
using System.Collections.Generic;
using System.Text;

using CK2Editor.Utility;


namespace CK2Editor
{
    public abstract class Entry
    {
        /// <summary>
        /// A user friendly name, supplied by the format file
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// The name used by the save file
        /// </summary>
        public string InternalName { get; set; }
        /// <summary>
        /// A link to a related section, supplied by the format file
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// The editor that edits this entry. Can be null if not part of an editors' tree
        /// </summary>
        public SectionEntry SectionEntry { get; set; }

        public List<SectionEntry> Sections { get; private set; }

        public List<ValueEntry> Values { get; private set; }

        public Entry()
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


        public SectionEntry Root { get; set; }


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
