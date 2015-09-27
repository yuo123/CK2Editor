using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CK2Editor.Editors;

namespace CK2Editor
{
    public struct ValueEntry
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
        /// The value type, string, number, series or null (misc.)
        /// </summary>
        public string Type;
        /// <summary>
        /// The stores value
        /// </summary>
        public string Value;
        /// <summary>
        /// A link to another section, supplied by the format file
        /// </summary>
        public IEditor Link;
    }
}
