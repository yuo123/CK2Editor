using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CK2Editor
{
    public class SectionEntry : Entry
    {
        /// <summary>
        /// The editor of this section
        /// </summary>
        public SectionEntry Section { get; set; }

        public SectionEntry()
        {

        }

        public SectionEntry(string internalName, string friendlyName, SectionEntry section, string link)
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Section = section;
            Link = link;
        }
    }
}
