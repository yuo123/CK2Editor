using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK2Editor.Editors;

namespace CK2Editor
{
    public struct SectionEntry
    {
        /// <summary>
        /// The name used in the save file
        /// </summary>
        public string InternalName;
        /// <summary>
        /// A user friendly name, supplied by the format file
        /// </summary>
        public string FriendlyName;
        /// <summary>
        /// The editor of this section
        /// </summary>
        public IEditor Section;
        /// <summary>
        /// A link to another section, supplied by the format file
        /// </summary>
        public string Link;

        public SectionEntry(string internalName, string friendlyName, IEditor section, string link)
        {
            InternalName = internalName;
            FriendlyName = friendlyName;
            Section = section;
            Link = link;
        }
    }
}
