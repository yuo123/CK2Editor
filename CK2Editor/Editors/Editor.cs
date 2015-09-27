using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK2Editor.Editors
{
    public class Editor : IEditor
    {
        protected FileSection raw;

        public IList<SectionEntry> Sections { get; set; }

        public IList<ValueEntry> Values
        { get; set; }

        public string RawText
        {
            get { return raw.ToString(); }
        }

        public void Save()
        {
            //recursively tell the sub-sections of this section to save
            foreach (SectionEntry section in Sections)
            {
                section.Section.Save();
            }

            //go through each of the values and update it
            foreach (ValueEntry value in Values)
            {
                switch (value.Type)
                {
                    case "string":
                        Util.ReplaceStringValue(raw, value.InternalName, value.Value);
                        break;
                    case "series":
                        FileSection series = Util.ExtractDelimited(raw, value.InternalName + "=");
                        series.Remove();
                        series.Insert(value.Value);
                        break;
                    default:
                        Util.ReplaceValue(raw, value.InternalName, value.Value);
                        break;
                }
            }
        }
    }
}
